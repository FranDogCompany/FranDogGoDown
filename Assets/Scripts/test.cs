using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    
    // ExcelManager em = new ExcelManager();
    ExcelManager em = null;
    void Start()
    {
        Debug.Log("123");
        em = new ExcelManager();
        string st = em.ExcelToJson("test.xlsx");
        Debug.Log("大功告成!" + st);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
