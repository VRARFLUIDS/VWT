using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FlowAnimator : MonoBehaviour
{
    AnimationMeshesMap animationMeshesMap;

    public float FramesPerSecond = 12;
    public bool PlayAnimation = true;
    public Material MaterialToUse;

    Mesh[] Meshes;
    protected MeshFilter MyMeshFilter;
    protected int FrameIndx = 0;
    
    public bool IsPlaying { get; private set; }

    void Start()
    {
        IsPlaying = false;

        animationMeshesMap = GetComponent<AnimationMeshesMap>();
        Meshes = animationMeshesMap.MeshesList;

        if (!CheckIfReady()) return;
        MyMeshFilter = GetComponent<MeshFilter>();
        MyMeshFilter.mesh = Meshes[0];
        GetComponent<MeshRenderer>().material = MaterialToUse;

        if (PlayAnimation)
            Play();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(IsPlaying)
                Stop();
            else
                Play();
        }
    }

    public void Play()
    {
        if (!CheckIfReady()) return;

        if (!IsPlaying)
        {
            IsPlaying = true;
            StartCoroutine(AnimateMesh());
        }
    }

    public void Stop()
    {
        if (!CheckIfReady()) return;

        if (IsPlaying)
        {
            IsPlaying = false;
            StopCoroutine("AnimateMesh");
        }
    }

    IEnumerator AnimateMesh()
    {
        var waitTime = 1/FramesPerSecond;

        while (IsPlaying)
        {
            if (FrameIndx > Meshes.Length - 1)
                FrameIndx = 0;

            MyMeshFilter.mesh = Meshes[FrameIndx];
            Debug.Log("Mesh index: " + FrameIndx );
            FrameIndx++;
            yield return new WaitForSeconds(waitTime);
        }
    }

    bool CheckIfReady()
    {
        if (Meshes.Length > 0)
            return true;
        Debug.LogError("No Meshes have been added to the Method1A");
        return false;
    }
}