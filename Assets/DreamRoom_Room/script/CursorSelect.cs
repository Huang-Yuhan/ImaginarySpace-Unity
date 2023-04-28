using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSelect : MonoBehaviour
{

    GameObject [] BottomPlanes;
    GameObject [] LeftPlanes;
    GameObject [] RightPlanes;
    // Start is called before the first frame update

    PuzzleGenerate puzzleGenerate;
    CreateCube createCube;
    void Start()
    {
        BottomPlanes=GameObject.FindGameObjectsWithTag("Bottom");
        LeftPlanes=GameObject.FindGameObjectsWithTag("Left");
        RightPlanes=GameObject.FindGameObjectsWithTag("Right");
        puzzleGenerate=gameObject.GetComponent<PuzzleGenerate>();
        createCube=gameObject.GetComponent<CreateCube>();
    }

    // Update is called once per frame
    void Update()
    {
        puzzleGenerate.materialUpdate();
        RaycastHit mhit;
        Ray mRay=Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(mRay,out mhit))
        {
            GameObject DestObj = mhit.collider.gameObject;
            if (DestObj.tag is "Bottom"
                or "Left"
                or "Right"
            )
            {
                DestObj.GetComponent<MeshRenderer>().material.color = new Color(224f/255,117f/255,117f/255);
                if (Input.GetMouseButtonDown(1))
                //如果鼠标右键按下就放置一个方块-旁边是plane
                {
                    Debug.Log("法向量:" + mhit.normal);
                    Debug.Log("鼠标点击的对象" + mhit.transform.parent.gameObject);
                    Vector3 WorldCorrdinate = DestObj.transform.position;
                    Vector3 LocalCorrdinate = transform.InverseTransformPoint(WorldCorrdinate)+transform.InverseTransformDirection(mhit.normal)*1.25f;
                    createCube.CubeGenerate(
                        LocalCorrdinate
                        );
                }
            }
            else if (DestObj.tag is "Bricks")       //旁边是方块
            {
                DestObj.GetComponent<MeshRenderer>().material.color = new Color(224f / 255, 117f / 255, 117f / 255);
                if (Input.GetMouseButtonDown(1))
                //如果鼠标右键按下就放置一个方块-旁边是plane
                {
                    Debug.Log("法向量:" + mhit.normal);
                    Debug.Log("鼠标点击的对象" + mhit.transform.parent.gameObject);
                    Vector3 WorldCorrdinate = DestObj.transform.position;
                    Vector3 LocalCorrdinate = transform.InverseTransformPoint(WorldCorrdinate) + transform.InverseTransformDirection(mhit.normal) * 2.5f;
                    createCube.CubeGenerate(
                        LocalCorrdinate
                        );
                }
                if (Input.GetMouseButtonDown(0))//右键按下删除方块
                {
                    Vector3 WorldCorrdinate = DestObj.transform.position;
                    Vector3 LocalCorrdinate = transform.InverseTransformPoint(WorldCorrdinate);
                    createCube.DeleteCube(
                        LocalCorrdinate
                        );
                }
            }
        }
    }
}
