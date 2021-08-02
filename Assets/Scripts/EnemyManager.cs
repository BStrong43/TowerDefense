using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    List<Enemy> enemies;

    [SerializeField]
    List<Transform> spawnLocations;

    public GameObject enemyPrefab;

    void Start()
    {
        enemies = new List<Enemy>();
        InitSpawnPoints();
    }

    void Update()
    {
        
    }

    public void SpawnWave()
    {
        foreach(Transform i in spawnLocations)
        {
            Vector3 pos = i.position;
            pos.y = 1;
            enemies.Add(GameObject.Instantiate(enemyPrefab, pos, Quaternion.identity).GetComponent<Enemy>());
        }
    }

    public int GetNumEnemies()
    {
        return enemies.Count;
    }

    public void RemoveEnemy(Enemy e)
    {
        enemies.Remove(e);
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }

    private void InitSpawnPoints()
    {
        spawnLocations = new List<Transform>();
        GameObject[] spawnLocs = GameObject.FindGameObjectsWithTag("Spawner");

        foreach(GameObject i in spawnLocs)
        {
            spawnLocations.Add(i.GetComponent<Transform>());
        }
    }
}
