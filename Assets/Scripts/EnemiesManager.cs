using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject player;
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();
    float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
        enemies.RemoveAll(x => x == null);
    }

    private void SpawnEnemy()
    {
        Vector2 position = GenerateRandomPosition();
        position.x += player.transform.position.x;
        position.y += player.transform.position.y;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);

        newEnemy.transform.parent = transform;
        enemies.Add(newEnemy);
        Debug.Log("enemy spawned. Current count: "+enemies.Count);
    }

    private Vector2 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if(UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }
        position.z = 0f;
            
        return position;
    }
}
