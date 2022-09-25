

[System.Serializable]
public class EnemyStatusModel
{
    public int items;
    public int ID;
    public string enemyName;
    public int LV1_HP;
    public int LV1_ATK;
    public int LV2_HP;
    public int LV2_ATK;
    public int LV3_HP;
    public int LV3_ATK;
    public int LV4_HP;
    public int LV4_ATK;
    public int LV5_HP;
    public int LV5_ATK;
    public int LV6_HP;
    public int LV6_ATK;
    public int LV7_HP;
    public int LV7_ATK;
    public int LV8_HP;
    public int LV8_ATK;
    public int LV9_HP;
    public int LV9_ATK;
    public int LV10_HP;
    public int LV10_ATK;

    [System.Serializable]
    public class EnemyStatusList
    {
        public EnemyStatusModel[] Table1;
    }
}
