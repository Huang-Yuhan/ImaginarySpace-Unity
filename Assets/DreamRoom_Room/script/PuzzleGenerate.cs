using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerate : MonoBehaviour
{
    // Start is called before the first frame update

    bool[,,] CubeData=new bool[4,4,4];
    GameObject [] BottomPlanes;
    GameObject [] LeftPlanes;
    GameObject [] RightPlanes;

    public Material DefaultMaterial;
    public Material HighlightMaterial;

    CreateCube createCube;

    /*
    dataGenerate()
    生成一个3D世界的4 x 4 x 4的物体
    */
    void dataGenerate()
    {
        for(int i=0;i<4;i++)
            for(int j=0;j<4;j++)
                for(int k=0;k<4;k++)
                    CubeData[i,j,k]=Random.Range(1,10)>1?false:true;
    }

    public void materialUpdate()
    {
        for(int i=0;i<4;i++)
            for(int k=0;k<4;k++)
            {
                bool IsNeedCube=false;
                for(int j=0;j<4;j++)IsNeedCube=IsNeedCube||CubeData[i,j,k];
                BottomPlanes[i*4+k].GetComponent<MeshRenderer>().material =IsNeedCube?HighlightMaterial:DefaultMaterial;
               // Debug.Log(BottomPlanes[i*4+j].GetComponent<MeshRenderer>().material);
            }

        for(int j=0;j<4;j++)
            for(int k=0;k<4;k++)
            {
                bool IsNeedCube=false;
                for(int i=0;i<4;i++)IsNeedCube=IsNeedCube||CubeData[i,j,k];
                LeftPlanes[j*4+k].GetComponent<MeshRenderer>().material =IsNeedCube?HighlightMaterial:DefaultMaterial;
               // Debug.Log(BottomPlanes[i*4+j].GetComponent<MeshRenderer>().material);
            }

        for(int i=0;i<4;i++)
            for(int j=0;j<4;j++)
            {
                bool IsNeedCube=false;
                for(int k=0;k<4;k++)IsNeedCube=IsNeedCube||CubeData[i,j,k];
                RightPlanes[i*4+j].GetComponent<MeshRenderer>().material =IsNeedCube?HighlightMaterial:DefaultMaterial; //i+j*4是编号的时候搞错了
               // Debug.Log(BottomPlanes[i*4+j].GetComponent<MeshRenderer>().material);
            }

        foreach (GameObject item in createCube.cubes)
        {
            item.GetComponent<MeshRenderer>().material = HighlightMaterial;
        }
    }

    bool CheckWin()
    {
        for (int i = 0; i < 4; i++)
            for (int k = 0; k < 4; k++)
            {
                bool IsNeedCube = false;
                bool IsThere = false;
                for (int j = 0; j < 4; j++)
                {
                    IsNeedCube = IsNeedCube || CubeData[i, j, k]; 
                    IsThere= IsThere || createCube.CubeData[i, j, k];
                }
                if (IsNeedCube && IsThere == false) return false;
                // Debug.Log(BottomPlanes[i*4+j].GetComponent<MeshRenderer>().material);
            }

        for (int j = 0; j < 4; j++)
            for (int k = 0; k < 4; k++)
            {
                bool IsNeedCube = false;
                bool IsThere = false;
                for (int i = 0; i < 4; i++)
                {
                    IsNeedCube = IsNeedCube || CubeData[i, j, k];
                    IsThere = IsThere || createCube.CubeData[i, j, k];
                }
                if (IsNeedCube && IsThere == false) return false;
                // Debug.Log(BottomPlanes[i*4+j].GetComponent<MeshRenderer>().material);
            }

        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                bool IsNeedCube = false;
                bool IsThere = false;
                for (int k = 0; k < 4; k++)
                {
                    IsNeedCube = IsNeedCube || CubeData[i, j, k];
                    IsThere = IsThere || createCube.CubeData[i, j, k];
                }
                if (IsNeedCube ^ IsThere ) return false;
            }
        return true;
    }

    void Start()
    {
        BottomPlanes=GameObject.FindGameObjectsWithTag("Bottom");
        LeftPlanes=GameObject.FindGameObjectsWithTag("Left");
        RightPlanes=GameObject.FindGameObjectsWithTag("Right");
        createCube = GetComponent<CreateCube>();

        foreach (GameObject item in BottomPlanes)
        {
            Debug.Log(item);
        }
        dataGenerate();

        materialUpdate();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckWin())
        {
            Debug.Log("WIN");
        }
        //else Debug.Log("Come on !");
    }
}
