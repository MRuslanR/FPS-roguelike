using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RandomSpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public List<Transform> spawnPoints;
    private BlockStorage storage;

    void Start()
    {
        storage = GameObject.Find("Storage").GetComponent<BlockStorage>();
        spawnPoints = new List<Transform>(spawnPoints);
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        
        int max_mobs = Random.Range(1, storage.cur_level);
        if (max_mobs > 8){
            max_mobs = 8;
        }
        for (int i = 0 ; i < max_mobs; i++){
            var spawn = Random.Range(0, spawnPoints.Count);
            var enemy_ind = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemy_ind], spawnPoints[spawn].transform.position, Quaternion.identity);
            spawnPoints.RemoveAt(spawn);
        }
    }
}
