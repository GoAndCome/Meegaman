using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMove : MonoBehaviour
{
    public GameObject Bullet_Normal;
    public GameObject Bullet_Bounce;
    public GameObject Bullet_Brick;
    public GameObject Bullet_Charge;

    public GameObject ChargingEffect;
    public GameObject Player_obj;


    public float fireDeley;
    public float ChargeTime = 0.0f;

    public bool RL_Chack_b = false;

    private float fireDeleyCheck;



    void Start()
    {
        fireDeleyCheck = fireDeley;
        ChargingEffect = Player_obj.transform.GetChild(1).gameObject;
    }
    
    void Update()
    {
        fireDeleyCheck += Time.deltaTime;

        switch (Player_obj.GetComponent<PlayerMove>().nowBulletType)
        {
            case 0:
                {
                    fireDeley = 0.2f;
                    break;
                }
            case 1:
                {
                    fireDeley = 0.5f;
                    break;
                }
            case 2:
                {
                    fireDeley = 1.0f;
                    break;
                }
            case 3:
                {
                    fireDeley = 3f;
                    break;
                }
        }

        if (Player_obj.transform.localScale == new Vector3(-0.8f, Player_obj.transform.localScale.y, Player_obj.transform.localScale.z))
        {
            RL_Chack_b = true;
        } else
        {
            RL_Chack_b = false;
        }

        if (fireDeleyCheck > fireDeley)
        {
            if (Player_obj.GetComponent<PlayerMove>().nowBulletType != 3) //차지불릿이 아닐 경우
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    /*Vector2 firePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    float angle = Mathf.Atan2(firePoint.y, firePoint.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = rotation;

                    Instantiate(Bullet_Pre, transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                    */

                    switch (Player_obj.GetComponent<PlayerMove>().nowBulletType)
                    {
                        case 0:
                            {
                                Instantiate(Bullet_Normal, transform.position, Quaternion.identity);
                                break;
                            }
                        case 1:
                            {
                                Instantiate(Bullet_Bounce, transform.position, Quaternion.identity);
                                break;
                            }
                        case 2:
                            {
                                Vector3 BrickVector = new Vector3(0, 0, 0);
                                if (!RL_Chack_b)
                                {
                                    BrickVector = new Vector3(transform.position.x + 3.0f, transform.position.y + 3.0f, transform.position.z);
                                }
                                else
                                {
                                    BrickVector = new Vector3(transform.position.x - 3.0f, transform.position.y + 3.0f, transform.position.z);
                                }
                                Instantiate(Bullet_Brick, BrickVector, Quaternion.identity);
                                break;
                            }
                        case 3:
                            {
                                Vector3 ChargeVector = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                                Instantiate(Bullet_Charge, ChargeVector, Quaternion.identity);
                                break;
                            }
                    }

                    fireDeleyCheck = 0.0f;
                }
            } 
            else if (Input.GetKey(KeyCode.X)) //차지불릿이고 차지를 시작했을 때
            {
                ChargingEffect.SetActive(true);
                if (ChargeTime <= 1.5f)
                {
                    ChargeTime += Time.deltaTime;
                }
            } else
            {
                if(Input.GetKeyUp(KeyCode.X)) //차지를 끝냈다면
                {
                    ChargingEffect.SetActive(false);
                    Instantiate(Bullet_Charge, transform.position, Quaternion.identity);
                    fireDeleyCheck = 0.0f;
                }
            }
        }
    }
}
