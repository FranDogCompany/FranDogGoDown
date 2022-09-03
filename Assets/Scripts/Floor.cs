using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //取得SystemCS的moveUpSpeed
        moveSpeed = GameObject.Find("EventSystem").GetComponent<SystemCS>().moveUpSpeed;

        //floor自動上移，為避免手機性能影響速度，要用Time.deltaTime,
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        //floor自中心點位置大於6格時，銷毀物件
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
            transform.parent.GetComponent<FloorManager>().SpawnFloor();
        }

    }
}
