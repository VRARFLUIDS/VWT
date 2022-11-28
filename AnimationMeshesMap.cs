using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class AnimationMeshesMap : MonoBehaviour
{
    public string GLTFFolderPath = "Assets/Resources/GLTFs/";
    public string GLTFGeneralName = "PUVWT_1-565.32";
    public int GLTFFirstFrame = 6000;
    public string GLTFextension = ".gltf";
    public int GLTFNameIncrementer= 10;
    public int NumberOfFrames = 11;
    public bool FillMeshesList = true;
    public bool ForceRefillList = false;
    
    public Mesh[] MeshesList;

    void Awake()
    {

        if (FillMeshesList)
        {
            if (IsReady()) 
            {
                if (ForceRefillList)
                {
                    Debug.Log("It was ready but now refilling meshes list");
                    MeshesPointer();
                    return;
                }
                Debug.Log("It was ready and meshes list was not modified");
                return;
            }
            Debug.Log("It was not ready but now refilling meshes list");
            MeshesPointer();
         }
    }

    bool IsReady()
    {
        if (MeshesList.Length > 0)
            return true;
        Debug.LogError("No Meshes have been added");
        return false;
    }

    void MeshesPointer()
    {
        MeshesList = new Mesh[NumberOfFrames];

        for (int i = 0; i < NumberOfFrames; i++)
        {
            int GLTFFrame = (GLTFFirstFrame + GLTFNameIncrementer*i);
            string GLTFCompleteName = string.Concat(GLTFFolderPath, GLTFGeneralName, GLTFFrame.ToString("0000"),GLTFextension);
            Debug.Log("Looking for Mesh of file: " + GLTFCompleteName);
        
            Object[] data = AssetDatabase.LoadAllAssetsAtPath(GLTFCompleteName);
            foreach (Object x in data)
            {
                if (x.name == "mesh1" && x is Mesh)
                {
                    Debug.Log("File found");
                    MeshesList[i]= x as Mesh;
                    break;
                }  
            }
        }
    }
}