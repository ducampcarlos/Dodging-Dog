using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float spawnRate = 1f;
    [SerializeField] Transform minX;
    [SerializeField] Transform maxX;
    [SerializeField] int poolSize = 20;

    private Coroutine spawnCoroutine;
    private List<GameObject> obstaclePool = new List<GameObject>();

    private void Start()
    {
        InitializePool();
        StartSpawning();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            GameObject obj = Instantiate(prefab);
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
        yield return new WaitForSeconds(spawnRate);
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void Spawn()
    {
        GameObject obstacle = GetPooledObject();
        if (obstacle != null)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(minX.position.x, maxX.position.x), transform.position.y, transform.position.z);
            obstacle.transform.position = spawnPosition;
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.SetActive(true);
        }
    }

    GameObject GetPooledObject()
    {
        foreach (GameObject obj in obstaclePool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        // Si todos están activos, opcionalmente instanciamos uno nuevo
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        obstaclePool.Add(newObj);
        return newObj;
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnCoroutine);
    }
}
