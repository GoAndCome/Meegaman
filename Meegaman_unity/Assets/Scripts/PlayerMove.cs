using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    enum FileBulletType
    {
        NORMAL = 0,
        BOUNCE = 1,
        BRICK = 2,
        CHARGE = 3
    };

    public static PlayerMove instance;

    Rigidbody2D player_Rigidbody;

    public GameObject PlayerHP_obj;
    public GameObject PlayerGun_obj;
    public GameObject gameOverPaner;

    public Text PlayerBulletCount_txt;

    public float jumpPower_F;
    public float RLPower_f;

    public int PlayerMaxHP = 100;
    public int hp_p;
    public int strikingPower = 10;
    public int nowBulletType = (int)FileBulletType.NORMAL;
    public int jumpCount = 0;

    public int PlayerHP
    {
        get
        {
            return hp_p;
        }
        set
        {
            hp_p = value;
            PlayerHPControl();
        }
    }

    void Start()
    {
        hp_p = PlayerMaxHP;
        player_Rigidbody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        PlayerBulletCount_txt.text = (nowBulletType + 1).ToString();
        PlayerBulletCount_txt.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (nowBulletType < (int)FileBulletType.CHARGE)
            {
                nowBulletType += 1;
            } else if(nowBulletType == (int)FileBulletType.CHARGE)
            {
                nowBulletType = (int)FileBulletType.NORMAL;
            }

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (jumpCount < 1)
            {
                Debug.Log(jumpCount);
                player_Rigidbody.velocity = Vector2.zero;

                Vector2 jumpVector = new Vector2(0, jumpPower_F);
                player_Rigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
                jumpCount++;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(0.8f, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x + RLPower_f * Time.deltaTime * 50, transform.position.y, transform.position.z);
            
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-0.8f, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x - RLPower_f * Time.deltaTime * 50, transform.position.y, transform.position.z);
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            transform.position += Vector3.up * Time.deltaTime * RLPower_f * 50f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            transform.position += Vector3.down * Time.deltaTime * RLPower_f * 50f;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nowBulletType = (int)FileBulletType.NORMAL;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nowBulletType = (int)FileBulletType.BOUNCE;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nowBulletType = (int)FileBulletType.BRICK;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            nowBulletType = (int)FileBulletType.CHARGE;
        }


    }
    public void PlayerHPControl()
    {
        PlayerHP_obj.GetComponent<Image>().fillAmount = (float)PlayerHP / (float)PlayerMaxHP;
        if (PlayerHP <= 0)
        {
            gameOverPaner.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌, HP : " + PlayerHP);
        if (collision.gameObject.tag == "Monster")
        {
            if (collision.gameObject.GetComponent<MonsterMove>().monsterKind == 2)
            {
                Destroy(collision.gameObject);
            }
            PlayerHP -= 20;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Wall")
        { }
        else
            jumpCount = 0;
    }
}
