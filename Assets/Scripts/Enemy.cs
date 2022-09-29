using UnityEngine;
using static EnemyStatusModel;


public class Enemy : MonoBehaviour
{
    public int HP = 1;  //怪物HP

    public int ATK = 1; //怪物攻擊力

    [SerializeField] float moveSpeed;   //怪物移動速度

    [SerializeField] string enemyTag;  //怪物種類

    [SerializeField] int level;   //關卡等級

    public EnemyStatusModel enemyStatusModel;    //存入怪物資料用

    public float timerTemp = 0f;    //計算轉向時機用

    public GameObject headPoint;     //判定頭部位置用

    //public GameObject feetPoint;       //玩家，取FeetPoint用

    //private float height = 0.0f;     //判斷feetPoint與headPoint的高度差

    //怪物數值List
    public EnemyStatusList myEnemyStatusList = new EnemyStatusList();


    void Start()
    {
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

        //讀取Excel取得怪物資料
        ExcelManager excelManager = gameObject.AddComponent<ExcelManager>();    //MonoBehaviour裡不能用new，要用AddComponent
        string json = excelManager.ExcelToJson("EnemyStatus");
        myEnemyStatusList = JsonUtility.FromJson<EnemyStatusList>(json);

        //蝙蝠
        if ("Enemy_Bat".Equals(enemyTag))
        {
            setStatus(myEnemyStatusList, "蝙蝠");
        }
        else if ("Enemy_Slime".Equals(enemyTag))
        {
            setStatus(myEnemyStatusList, "史萊姆");
        }
    }

    //塞值到HP、ATK
    public void setStatus(EnemyStatusList list, string enemyName)
    {
        for (int i = 0; i < list.Table1.GetLength(0); i++)
        {
            if ((enemyName).Equals(list.Table1[i].enemyName))
            {
                HP = list.Table1[i].BASIC_HP + (list.Table1[i].LvUp_HP * level);
                ATK = list.Table1[i].BASIC_ATK + (list.Table1[i].LvUp_ATK * level);
                break;
            }
        }
    }

    //扣血邏輯
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
}
