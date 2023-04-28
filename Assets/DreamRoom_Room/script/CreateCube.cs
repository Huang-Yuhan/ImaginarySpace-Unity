using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefabs;
    public bool [,,] CubeData=new bool[4,4,4];
    public List<GameObject> cubes;
    public int PositionToIndex(int x)=> x switch
    {
        -15 => 0,
        -5 => 1,
        5 => 2,
        15 => 3,
        _ => -1,
    };

    string TestStr(int x, int y, int z)
    {
        if (x==0)
        {
            return (y*4+z).ToString();
        }
        if(y==0)return (x*4+z).ToString();
        if(z==0)return (y*4+z).ToString();
        return "";
    }

    //position是相对于父节点的坐标
    public void CubeGenerate(Vector3 position)
    {

        int x, y, z;
        Debug.Log(position);
        x = PositionToIndex((position.x * 4).ConvertTo<int>());             //float会有精度问题
        y = PositionToIndex((position.y * 4).ConvertTo<int>());
        z = PositionToIndex((position.z * 4).ConvertTo<int>());
        if (x < 0 || y < 0 || z < 0) return;
        if (CubeData[x, y, z]) return;

        GameObject objPrefabs=GameObject.Instantiate(prefabs,transform,false);
        objPrefabs.transform.localPosition = position;
        cubes.Add(objPrefabs);
        //false保证相对父节点
        /*objPrefabs.name="("+x.ToString()+","+y.ToString()+","+z.ToString()+")"+TestStr(x,y,z);  
        objPrefabs.transform.localScale = Vector3.one*0.4f;*/
        CubeData[x,y,z]=true;
    }

    public void DeleteCube(Vector3 position)
    {
        int x, y, z;
        x = PositionToIndex((position.x * 4).ConvertTo<int>());
        y = PositionToIndex((position.y * 4).ConvertTo<int>());
        z = PositionToIndex((position.z * 4).ConvertTo<int>());
        if (CubeData[x, y, z] is false)
        {
            Debug.Log(position);
        }
        CubeData[x, y, z] = false;
        int DelIndex = -1;
        for (int i = 0; i < cubes.Count; i++)
            if (cubes[i].transform.localPosition==position)
            {
                DelIndex = i;
                break;
            }
        if (DelIndex!=-1)
        {
            Destroy(cubes[DelIndex]);
            cubes.RemoveAt(DelIndex);
        }
    }

    void Test()
    {
        float[] nums = { -3.75f, -1.25f, 1.25f, 3.75f };
        for (int i = 0; i < 4; i++)
            for(int j=0;j<4;j++)
                for(int k=0;k<4;k++)
                    CubeGenerate(new Vector3(nums[i], nums[j], nums[k]));
    }

    void Start()
    {
        for(int i=0;i<4;i++)
        for(int j=0;j<4;j++)
        for(int k=0;k<4;k++)
        CubeData[i,j,k]=false;
        //Test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
