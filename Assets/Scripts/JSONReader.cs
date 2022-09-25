using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class Player
    {
        public string ID; // ID
        public string Name; // 怪物名稱
        public int LV1_HP; // LV1_HP
        public int LV1_ATK; // LV1_ATK
        public int LV2_HP; // LV2_HP
        public int LV2_ATK; // LV2_ATK
        public int LV3_HP; // LV3_HP
        public int LV3_ATK; // LV3_ATK
        public int LV4_HP; // LV4_HP
        public int LV4_ATK; // LV4_ATK
        public int LV5_HP; // LV5_HP
        public int LV5_ATK; // LV5_ATK
        public int LV6_HP; // LV6_HP
        public int LV6_ATK; // LV6_ATK
        public int LV7_HP; // LV7_HP
        public int LV7_ATK; // LV7_ATK
        public int LV8_HP; // LV8_HP
        public int LV8_ATK; // LV8_ATK
        public int LV9_HP; // LV9_HP
        public int LV9_ATK; // LV9_ATK
        public int LV10_HP; // LV10_HP
        public int LV10_ATK; // LV10_ATK
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] Player;
    }

    public PlayerList myEnemyStatusList = new PlayerList();

    // Start is called before the first frame update
    void Start()
    {
        myEnemyStatusList = JsonUtility.FromJson<PlayerList>(textJSON.text);
        Debug.Log("煩死我了!@!!!!!" + myEnemyStatusList.Player[0].Name);

    }

}
