using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Pooling")]
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] int poolSize = 10;
    private List<GameObject> obstaclePool;

    [Header("Spawn Settings")]
    [SerializeField] float spawnRate = 1f;
    [SerializeField] float spawnRateDecrease = 0.05f;
    [SerializeField] float minSpawnRate = 0.3f;
    [SerializeField] Transform minX;
    [SerializeField] Transform maxX;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        CreatePool();
        StartSpawning();
    }

    void CreatePool()
    {
        obstaclePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            obj.SetActive(false);
            obstaclePool.Add(obj);
        }
    }

    void StartSpawning()
    {
        spawnCoroutine = StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnRate);
            if (spawnRate > minSpawnRate)
            {
                spawnRate -= spawnRateDecrease;
            }
        }
    }

    void Spawn()
    {
        GameObject obj = GetPooledObstacle();
        if (obj != null)
        {
            obj.transform.position = new Vector3(
                Random.Range(minX.position.x, maxX.position.x),
                transform.position.y,
                transform.position.z
            );
            obj.SetActive(true);
        }
    }

    GameObject GetPooledObstacle()
    {
        foreach (GameObject obj in obstaclePool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        // Expand pool si hace falta
        GameObject newObj = Instantiate(obstaclePrefab);
        newObj.SetActive(false);
        obstaclePool.Add(newObj);
        return newObj;
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }
}
