using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SystemCS : MonoBehaviour
{
    public int level = 1;   //關卡等級

    public int floor = 1;   //樓層數

    public float levelUpTime = 0f;      //關卡升級所需時間

    public float timerTemp = 0f;    //計算目前關卡遊戲時間用

    public float totalTimer = 0f;    //計算總遊戲時間用

    public float moveUpSpeed = 0.0f;   //普通地板、針型地板、金幣等物件往上的速度;

    public  AudioSource myAudioSource;  //音效控管

    public AudioClip stepOnHead;    //音效_踩頭部

    // Start is called before the first frame update
    void Start()
    {
        // Sounds
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        totalTimer += Time.deltaTime;
        levelUpdate();
        levelUpCounter();
    }

    void levelUpdate()
    {
        switch (level)
        {
            case 1:
                //moveUpSpeed = 1.0f;
                moveUpSpeed = 0f;     //測試用，停止上升
                levelUpTime = 15f;
                break;
            case 2:
                moveUpSpeed = 1.5f;
                levelUpTime = 20f;
                break;
            case 3:
                moveUpSpeed = 2.0f;
                levelUpTime = 30f;
                break;
            case 4:
                moveUpSpeed = 2.5f;
                levelUpTime = 60f;
                break;
            case 5:
                moveUpSpeed = 3.0f;
                levelUpTime = 225f;
                break;
            case 6:
                moveUpSpeed = 3.5f;
                levelUpTime = 225f;
                break;
            case 7:
                moveUpSpeed = 4.0f;
                levelUpTime = 225f;
                break;
            case 8:
                moveUpSpeed = 4.5f;
                levelUpTime = 200f;
                break;
            case 9:
                moveUpSpeed = 5.0f;
                levelUpTime = 200f;
                break;
            case 10:
                moveUpSpeed = 5.5f;
                break;
        }
    }

    void levelUpCounter()
    {
        timerTemp += Time.deltaTime;

        //如果timerTemp到達關卡升級所需時間
        if (timerTemp >= levelUpTime)
        {
            level++;
            timerTemp = 0f;
        }
    }
}
