using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] GameObject moneyPrefabs;

    [SerializeField] int level;   //取SystemCS的關卡等級用

    [SerializeField] float moneyPercentage;     //money出現的機率

    [SerializeField] float randomFloat;     //1~100隨機到的數字

    void Start()
    {
        //取得目前level
        level = GameObject.Find("EventSystem").GetComponent<SystemCS>().level;
    }

    // Update is called once per frame
    void Update()
    {
        //取得目前level
        level = GameObject.Find("EventSystem").GetComponent<SystemCS>().level;

        moneyPercentageSet();
        generateMoney();

    }

    void moneyPercentageSet()
    {
        //各level出現money的機率
        switch (level)
        {
            case 1:
                moneyPercentage = 0.92f;
                break;
            case 2:
                moneyPercentage = 0.03f;
                break;
            case 3:
                moneyPercentage = 0.04f;
                break;
            case 4:
                moneyPercentage = 0.05f;
                break;
            case 5:
                moneyPercentage = 0.06f;
                break;
            case 6:
                moneyPercentage = 0.07f;
                break;
            case 7:
                moneyPercentage = 0.08f;
                break;
            case 8:
                moneyPercentage = 0.09f;
                break;
            case 9:
                moneyPercentage = 0.1f;
                break;
            case 10:
                moneyPercentage = 0.15f;
                break;
        }
    }

    void generateMoney()
    {
        randomFloat = Random.Range(0f, 100f);

        //如果randomFloat小於moneyPercentage，表示擲到成功生成money的機率
        if (randomFloat < moneyPercentage)
        {
            GameObject money = Instantiate(moneyPrefabs, transform); //實例化物件
            money.transform.position = new Vector3(Random.Range(-1.8f, 1.8f), -6f, 0f); //隨機移動money位置
        }

    }
}
