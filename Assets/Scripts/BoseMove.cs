using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoseMove : MonoBehaviour
{
    public GameObject BoseSpakes_obj;
    public GameObject BoseShat_obj;

    GameObject Player_obj;

    float PatternTime;
    float PatternEndTime;
    int WhatCheck;

    void Start()
    {
        Player_obj = GameObject.Find("Player");

        PatternTime = 0.0f;
        PatternEndTime = 0.5f;
        WhatCheck = 999;
    }

    void Update()
    {
        PatternTime += 0.01f;

        if(PatternTime >= PatternEndTime)
        {
            

            int PreviousPattern = WhatCheck;

            while (true)
            {
                WhatCheck = (int)Random.Range(1.0f, 2.9f);
                if (WhatCheck != PreviousPattern)
                {
                    break;
                }
            }

            switch(WhatCheck)
            {
                case 1:
                    PatternStart_Spakes();
                    break;

                case 2:
                    PatternStart_Shat();
                    break;
            }

            PatternTime = 0.0f;
        }
    }

    void PatternStart_Spakes()
    {
        float spakes_x;

        spakes_x = Random.Range(-2.0f, 2.3f);

        PatternEndTime = 1.5f;

        GameObject.Instantiate(BoseSpakes_obj, new Vector3(spakes_x, -9.7f, 0f), Quaternion.identity);
    }

    void PatternStart_Shat()
    {
        PatternEndTime = 3f;

        GameObject.Instantiate(BoseShat_obj, new Vector3(11f, Player_obj.transform.position.y, 0), Quaternion.identity);
        GameObject.Instantiate(BoseShat_obj, new Vector3(-11f, Player_obj.transform.position.y, 0), Quaternion.identity);
    }
}