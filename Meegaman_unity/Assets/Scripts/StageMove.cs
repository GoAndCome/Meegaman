using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    public List<GameObject> MonsterTrue_list = new List<GameObject>();
    public List<GameObject> MonsterFalse_list = new List<GameObject>();

    public GameObject[] DeleteList;
    
    public GameObject Player_obj;
    public GameObject Camera_obj;
    public GameObject NextStageMoveWall_obj;
    public GameObject DeleteObj_obj;

    public float CameraMovexy;

    public bool xymove = false;


    float TemporaryTime_f = 1.0f;
    float TemporaryTimeCount_f = 0.0f;

    bool CameraMove_bool = false;

    void Start()
    {
        TemporaryTimeCount_f = 0;
        Player_obj = GameObject.Find("Player");
        Camera_obj = GameObject.Find("Main Camera");
    }

    [System.Obsolete]
    void Update()
    {
        if (CameraMove_bool)
        {
            TemporaryTimeCount_f += 0.1f;
            if (TemporaryTimeCount_f >= 10.0f)
            {
                DestroySelf();
            }

            if (!xymove) //false = x축 움직임, true = y축 움직임 
            {

                if (Camera_obj.transform.position.x - CameraMovexy < 0) //음수일 경우 (오른쪽으로 움직여야 할 경우)
                {
                    if (Camera_obj.transform.position.x > CameraMovexy)
                    {
                        Camera_obj.transform.position = new Vector3(CameraMovexy, Camera_obj.transform.position.y, -10f);
                    }
                    else
                    {
                        Camera_obj.transform.position = new Vector3(Camera_obj.transform.position.x + 
                            (TemporaryTimeCount_f * 0.04f), Camera_obj.transform.position.y, -10f);
                    }
                }
                else if (Camera_obj.transform.position.x - CameraMovexy > 0) //양수일 경우 (왼쪽으로 움직여야 할 경우)
                {
                    if (Camera_obj.transform.position.x <= CameraMovexy)
                    {
                        Camera_obj.transform.position = new Vector3(CameraMovexy, Camera_obj.transform.position.y, -10f);
                    }
                    else
                    {
                        Camera_obj.transform.position = new Vector3(Camera_obj.transform.position.x - (TemporaryTimeCount_f * 0.04f), Camera_obj.transform.position.y, -10f);
                    }
                }
            } else
            {
                if (Camera_obj.transform.position.y - CameraMovexy < 0) //음수일 경우 (오른쪽으로 움직여야 할 경우)
                {
                    if (Camera_obj.transform.position.y > CameraMovexy)
                    {
                        Camera_obj.transform.position = new Vector3(Camera_obj.transform.position.x, CameraMovexy, -10f);
                    }
                    else
                    {
                        Camera_obj.transform.position = new Vector3(Camera_obj.transform.position.x, Camera_obj.transform.position.y + (TemporaryTimeCount_f * 0.022f), -10f);
                    }
                }
                else if (Camera_obj.transform.position.y - CameraMovexy > 0) //양수일 경우 (왼쪽으로 움직여야 할 경우)
                {
                    if (Camera_obj.transform.position.y <= CameraMovexy)
                    {
                        Camera_obj.transform.position = new Vector3(Camera_obj.transform.position.x, CameraMovexy, -10f); ;
                    }
                    else
                    {
                        Camera_obj.transform.position = new Vector3(Camera_obj.transform.position.x, Camera_obj.transform.position.y - (TemporaryTimeCount_f * 0.022f), -10f);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Wall" && collision.gameObject.tag != "Bullet")
        {
            //Camera_obj.transform.position = new Vector3(19.5f, 0, -10f);
            CameraMove_bool = true;
            Time.timeScale = 0f;
            Debug.Log("충돌중");
        }
    }

    [System.Obsolete]
    private void DestroySelf()
    {
        for (int i = 0; i < MonsterFalse_list.Count; i++)
        {
            GameObject[] monobj_ = GameObject.FindGameObjectsWithTag("Monster");
            for(int j = 0; j < monobj_.Length; j++)
            {
                if(monobj_[j].name == "JUDOTAN(Clone)")
                    Destroy(monobj_[j]);
            }
            
            if (MonsterFalse_list[i] != null)
                MonsterFalse_list[i].SetActive(false);
        }

        Time.timeScale = 1f;
        Debug.Log("발동");

        for (int i = 0; i < MonsterTrue_list.Count; i++)
        {
            if (MonsterTrue_list[i] != null)
                MonsterTrue_list[i].SetActive(true);
            
        }

        gameObject.SetActive(false);
        NextStageMoveWall_obj.SetActive(true);
        CameraMove_bool = false;
        TemporaryTimeCount_f = 0;
    }
}
