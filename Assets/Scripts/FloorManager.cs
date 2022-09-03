using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabs;

    public void SpawnFloor(){
        int r = Random.Range(0, floorPrefabs.Length);   //從floorPrefabs隨機取第X個物件
        GameObject floor = Instantiate(floorPrefabs[r], transform); //實例化物件
        floor.transform.position = new Vector3(Random.Range(-1.8f, 1.8f), -6f, 0f); //隨機移動floor位置
    }
}
