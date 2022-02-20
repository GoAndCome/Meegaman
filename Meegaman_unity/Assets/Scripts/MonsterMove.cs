using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    enum MonsterName
    {
        KUNGKUNGLEE = 0,
        JUDOTANFIRE = 1,
        JUDOTAN = 2,
        BOSESPAKES = 3,
        BOSESHAT = 4
    };
    
    public GameObject judotan_obj;

    public float SummonsJugotanTime = 0f;
    public float JudotanSpeed_f = 0.05f;

    public int monsterKind;

    int monsterBasicsHP = 10;
    int hp;

    bool xPlus = true;

    GameObject Player_obj;

    public int monsterNowHP
    {
        get
        {
            return hp;
        }
        set 
        {
            hp = value;
            if(hp <= 0)
            {
                MonsterDie();
            }
        }

    }
        
    void Start()
    {
        Player_obj = GameObject.Find("Player");

        switch (monsterKind)
        {
            case (int)MonsterName.KUNGKUNGLEE:
                monsterNowHP = monsterBasicsHP * 3;
                break;
            case (int)MonsterName.JUDOTANFIRE:
                monsterNowHP = monsterBasicsHP;
                break;
            case (int)MonsterName.JUDOTAN:
                monsterNowHP = monsterBasicsHP;
                break;
            case (int)MonsterName.BOSESPAKES:
                break;
            case (int)MonsterName.BOSESHAT:
                if(gameObject.transform.position.x < 0)
                {
                    xPlus = false;
                }
                break;

        }
    }

    private void FixedUpdate()
    {
        SummonsJugotanTime += Time.deltaTime;

        if (monsterKind == (int)MonsterName.JUDOTAN)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player_obj.transform.position, JudotanSpeed_f);
        }

        if (monsterKind == (int)MonsterName.JUDOTANFIRE && SummonsJugotanTime >= 1.5f)
        {
            SummonsJugotanTime = 0f;
            if (judotan_obj != null)
            {
                GameObject.Instantiate(judotan_obj, gameObject.transform.position, Quaternion.identity).transform.parent = gameObject.transform;
            }
        }

        if(monsterKind == (int)MonsterName.BOSESPAKES)
        {
            gameObject.transform.position += new Vector3(0, -Time.deltaTime * 10, 0);

            if(gameObject.transform.position.y <= -20.0)
            {
                Destroy(gameObject, 1f);
            } 
        }

        if(monsterKind == (int)MonsterName.BOSESHAT)
        {
            if(xPlus) //x == 11
            {
                gameObject.transform.position += new Vector3(-Time.deltaTime * 15, 0, 0);

                if (gameObject.transform.position.x <= -11f)
                {
                    Destroy(gameObject);
                }
            }

            if (!xPlus && SummonsJugotanTime >= 0.5f) //x == -11
            {
                gameObject.transform.position += new Vector3(Time.deltaTime * 15, 0, 0);
                
                if (gameObject.transform.position.x >= 11f)
                {
                    Destroy(gameObject);
                }
            }
        }

    }

    void Update()
    {
        
    }

    void MonsterDie()
    {
        Destroy(gameObject);
    }

    
}
