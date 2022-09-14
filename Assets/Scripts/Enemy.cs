using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyStatusModel;

public class Enemy : MonoBehaviour
{
    public int HP = 1;  //怪物HP

    public int ATK = 1; //怪物攻擊力

    [SerializeField] float moveSpeed;   //怪物移動速度

    [SerializeField] string enemyTag;  //怪物種類

    [SerializeField] int level;   //關卡等級

    public ArrayData[] enemyStatusModel;    //存入怪物資料用
    public float timerTemp = 0f;    //計算轉向時機用

    public GameObject headPoint;     //判定頭部位置用

    //public GameObject feetPoint;       //玩家，取FeetPoint用

    //private float height = 0.0f;     //判斷feetPoint與headPoint的高度差


    void Start()
    {
        enemyStatusModel = new ArrayData[10];
        getStatus();
    }

    void Update()
    {
        //取得SystemCS的moveUpSpeed
        moveSpeed = GameObject.Find("EventSystem").GetComponent<SystemCS>().moveUpSpeed;

        moveMode();
    }

    void moveMode()
    {
        timerTemp += Time.deltaTime;
        //如果timerTemp到達關卡升級所需時間
        if (timerTemp <= 2)
        {
            //floor自動上移，為避免手機性能影響速度，要用Time.deltaTime,
            transform.Translate(moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime * 1.5f, 0);
            GetComponent<SpriteRenderer>().flipX = true;    //控制圖像左右方向
        }
        else if (timerTemp > 2 && timerTemp <= 4)
        {
            transform.Translate(-moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime * 1.5f, 0);
            GetComponent<SpriteRenderer>().flipX = false;    //控制圖像左右方向
        }
        else if (timerTemp > 4)
        {
            timerTemp = 0f;
        }
    }

    //取得怪物素質
    public void getStatus()
    {
        //取得物件tag
        enemyTag = this.gameObject.tag;

        //取得SystemCS的level
        level = GameObject.Find("EventSystem").GetComponent<SystemCS>().level;

        //TODO 讀取Excel取得怪物資料
        ExcelManager excelManager = new ExcelManager();
        string json = excelManager.ExcelToJson("EnemyStatus.xlsx");
        Debug.Log(json);
        enemyStatusModel = JsonHelper.FromJson<ArrayData>(json);
        Debug.Log("測試~~~" + enemyStatusModel[0].enemyName);
        Debug.Log("測試~~~" + enemyStatusModel[1].enemyName);

        //蝙蝠
        if ("Enemy_Bat".Equals(enemyTag))
        {
            switch (level)
            {
                case 1:
                    HP = 10;
                    ATK = 15;
                    break;
                case 2:
                    HP = 20;
                    ATK = 30;
                    break;
                case 3:
                    HP = 30;
                    ATK = 45;
                    break;
                case 4:
                    HP = 40;
                    ATK = 60;
                    break;
                case 5:
                    HP = 50;
                    ATK = 75;
                    break;
                case 6:
                    HP = 60;
                    ATK = 90;
                    break;
                case 7:
                    HP = 70;
                    ATK = 105;
                    break;
                case 8:
                    HP = 80;
                    ATK = 120;
                    break;
                case 9:
                    HP = 90;
                    ATK = 135;
                    break;
                case 10:
                    HP = 100;
                    ATK = 150;
                    break;
            }

        }
    }

    //後來改寫在Player.cs
    //當頭部被踩到
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         //如果feetPoint的位置比headPoint高，表示有踩到頭
    //         height = feetPoint.transform.position.y - headPoint.transform.position.y;
    //         if (height > 0)
    //         {
    //             other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2.0f, ForceMode2D.Impulse);
    //         }
    //     }

    // }

    public void reduceHP(int ATK)
    {
        Debug.Log("HP一開始是" + HP);
        HP = HP - ATK;
        Debug.Log("HP後來是" + HP);

        //如果HP低於0，Enemy物件消失
        if (HP <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
