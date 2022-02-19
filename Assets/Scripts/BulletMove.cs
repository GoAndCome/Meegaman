using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BulletMove : MonoBehaviour
{
    public float moveSpeed;
    public float LifeTime_f;

    public int Bullet_i = 0;
    public int monsterAttackDamege = 10;

    public bool RL_Chack_b = false; //f = 오른쪽, t = 왼쪽
    public bool thisBulletType = false;

    public GameObject Player_obj;
    public GameObject PlayerGun_obj;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, LifeTime_f);
        Player_obj = GameObject.Find("Player");
        PlayerGun_obj = GameObject.Find("PlayerGun");

        if (Player_obj.transform.localScale == new Vector3(-0.8f, Player_obj.transform.localScale.y, Player_obj.transform.localScale.z))
        {
            RL_Chack_b = true; //왼쪽
        }

        switch (Bullet_i)
        {
            case 0:
                {
                    monsterAttackDamege = 10;
                    break;
                }
            case 1:
                {
                    monsterAttackDamege = 20;
                    break;
                }
            case 2:
                {
                    monsterAttackDamege = 30;
                    break;
                }
            case 3:
                {
                    float damege_f = PlayerGun_obj.GetComponent<GunMove>().ChargeTime * 40;
                    monsterAttackDamege = (int)damege_f; //최대값 60
                    break;
                }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!thisBulletType)
        {
            BulletTypePeg();
        }

        if (Bullet_i != 2)
        {
            if (RL_Chack_b)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }


    }

    public void BulletTypePeg() //불릿 타입을 고정시킴
    {
        Bullet_i = Player_obj.GetComponent<PlayerMove>().nowBulletType;
        thisBulletType = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌");
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<MonsterMove>().monsterNowHP -= monsterAttackDamege;
            Destroy(gameObject);
        }

        if (Bullet_i != 1)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Bullet_i != 1)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }

}
