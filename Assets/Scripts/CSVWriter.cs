using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filePath = "";

    [System.Serializable]
    public class E_Status
    {
        public int id;
        public string enemyName;
        public int HP;
        public int ATK;
    }
    [System.Serializable]
    public class E_StatusList
    {
        public E_Status[] eStatus;
    }

    public E_StatusList eStatusList = new E_StatusList();


    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.streamingAssetsPath + "/test2.xlsx";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("123");
            WriteCSV();
        }
    }

    public void WriteCSV()
    {
        TextWriter tw = new StreamWriter(filePath, false);
        tw.WriteLine("id, enemyName, HP, ATK");
        tw.Close();

        tw = new StreamWriter(filePath, true);

        for (int i = 0; i < eStatusList.eStatus.Length; i++)
        {
            tw.WriteLine(eStatusList.eStatus[i].id + "," +
            eStatusList.eStatus[i].enemyName + "," +
            eStatusList.eStatus[i].HP + "," +
            eStatusList.eStatus[i].ATK);
        }
        tw.Close();
    }
}
