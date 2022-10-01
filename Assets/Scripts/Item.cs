using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //floor自中心點位置大於6格時，銷毀物件
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }
}
