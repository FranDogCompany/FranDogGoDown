using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleInputNamespace;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    GameObject currentFloor;

    [SerializeField] int maxHp;

    [SerializeField] int currentHp;

    public PlayerHP HpBar;

    public Text scoreText;

    public Text floorText;

    public int score;

    float scoreTime;    //分數用計時器

    public int floor;

    float floorTime;    //層數用計時器

    AudioSource deathSound;

    [SerializeField] GameObject replayButton;

    private int enemyAtk = 0;   //敵人的攻擊力

    //取得虛擬搖桿的移動距離
    float xAxis = 0.0f;
    float yAxis = 0.0f;

    //是否踩在地上
    bool isOnFloor = false;

    //裝音效用
    public AudioClip gameSound;


    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        HpBar.SetMaxHealth(maxHp);
        score = 0;
        scoreTime = 0f;
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //鍵盤移動控制
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            GetComponent<Animator>().SetBool("run", false);
        }

        UpdateScore();
        UpdateFloor();

        //手機移動控制
        xAxis = GameObject.Find("Joystick").GetComponent<JoystickCS>().xAxis.value;
        yAxis = GameObject.Find("Joystick").GetComponent<JoystickCS>().yAxis.value;
        JoystickMove(xAxis, yAxis);
    }

    //Trigger判定
    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果跌到谷底
        if (other.gameObject.tag == "DeathLine")
        {
            Debug.Log("你輸了");
            Die();
        }
        //改用Collision
        // else if (other.gameObject.tag == "Enemy")
        // {
        //     string enemyType = other.gameObject.name;   //取得怪物的name
        //     other.GetComponent<Enemy>().getStatus();
        //     if ("Enemy_Bat".Equals(enemyType) || "Enemy_Bat (1)".Equals(enemyType))
        //     {
        //         enemyAtk = other.GetComponent<Enemy>().ATK;
        //         Debug.Log("碰到蝙蝠!ATK = " + enemyAtk);
        //         other.gameObject.GetComponent<AudioSource>().Play();
        //         ModifyHp(-3);   //扣3滴血
        //         GetComponent<Animator>().SetTrigger("hurt");

        //     }
        // }
    }


    //撞擊判定
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "NormalFloor")
        {
            //如果法線、法向量往上的話(0f, 1f)
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = other.gameObject;
                ModifyHp(1);    //回1滴血
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (other.gameObject.tag == "NailsFloor")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = other.gameObject;
                ModifyHp(-3);   //扣3滴血
                other.gameObject.GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetTrigger("hurt");
                isOnFloor = true;
            }
        }
        else if (other.gameObject.tag == "Ceiling")
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            //如果碰到天花板，就讓現在碰到的物件currentFloor的BoxCollider2D取消
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;

            ModifyHp(-3);     //扣3滴血
            GetComponent<Animator>().SetTrigger("hurt");
        }
        else if (other.gameObject.tag.Contains("Enemy"))   //TODO 踩踏Enemy頭部判定
        {
            //如果法線、法向量往上的話(0f, 1f)
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                //Player彈起
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
                other.collider.GetComponent<Animator>().SetTrigger("Hit");
                
                //踩頭音效
                gameSound = GameObject.Find("EventSystem").GetComponent<SystemCS>().stepOnHead;
                GameObject.Find("EventSystem").GetComponent<SystemCS>().myAudioSource.PlayOneShot(gameSound);
                //TODO 還有音太大聲、扣HP、死亡等等
            }else{
                enemyAtk = other.collider.GetComponent<Enemy>().ATK;
                other.gameObject.GetComponent<AudioSource>().Play();
                ModifyHp(-enemyAtk);   //扣血
                GetComponent<Animator>().SetTrigger("hurt");
            }
        }
    }

    void ModifyHp(int num)
    {
        currentHp += num;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        else if (currentHp < 0)
        {
            currentHp = 0;
            Die();
        }
        UpdateHpBar();    //更新Hp
    }


    void UpdateHpBar()
    {
        HpBar.SetHealth(currentHp);
    }

    //更新分數
    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 2)
        {
            score += 10;
            scoreTime = 0f;
        }
        scoreText.text = "Score " + score.ToString();
    }

    //更新樓層數
    void UpdateFloor()
    {
        floorTime += Time.deltaTime;
        if (floorTime > 10)
        {
            floor++;
            floorTime = 0f;
            floorText.text = "樓層 " + floor.ToString();
        }
    }

    void Die()
    {
        deathSound.Play();
        Time.timeScale = 0f;    //遊戲暫停
        replayButton.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale = 1f;    //解除遊戲暫停
        SceneManager.LoadScene("SampleScene");
    }

    //類比搖桿
    public void JoystickMove(float xAxis, float yAxis)
    {
        //類比搖桿_右移動
        if (xAxis > 0.4)
        {
            //移動動畫
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("run", true);

            //腳色移動
            float speed = GameObject.Find("Player").GetComponent<Player>().moveSpeed;   //取得腳本Player腳本上的moveSpeed值(記得要放在同一資料夾才能找到)
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        //類比搖桿_左移動
        if (xAxis < -0.4)
        {
            //移動動畫
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("run", true);

            //腳色移動
            float speed = GameObject.Find("Player").GetComponent<Player>().moveSpeed;   //取得腳本Player腳本上的moveSpeed值(記得要放在同一資料夾才能找到)
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        //類比搖桿_上移動
        if (yAxis > 0.4 && isOnFloor)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = 5f * Vector2.up;
        }

        // //類比搖桿_下移動(先註解，暫時不用)
        // if (yAxis < -0.5)
        // {
        // 	float speed = GameObject.Find("Player").GetComponent<Player>().moveSpeed;   //取得腳本Player腳本上的moveSpeed值(記得要放在同一資料夾才能找到)

        // 	this.transform.position -= new Vector3(0, speed*Time.deltaTime, 0);
        // }
    }

    //判定是否踩在地上或針上
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "NormalFloor" || other.gameObject.tag == "NailsFloor")
        {
            isOnFloor = true;
        }
    }

    //當主角離開地上或針上
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "NormalFloor" || other.gameObject.tag == "NailsFloor")
        {
            isOnFloor = false;
        }
    }
}
