using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] float moveSpeed;


    // Update is called once per frame
    void Update()
    {
        //取得SystemCS的moveUpSpeed
        moveSpeed = GameObject.Find("EventSystem").GetComponent<SystemCS>().moveUpSpeed;

        //金幣自動上移，為避免手機性能影響速度，要用Time.deltaTime,
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        //金幣自中心點位置大於6格時，銷毀物件
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }

    //撞擊判定
    void OnTriggerEnter2D(Collider2D other)
    {
        //主角碰到時獲取100score
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
            GameObject.Find("Player").GetComponent<Player>().score += 100;
        }
    }
}
