using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject[] normalSawVariants;
    [SerializeField] GameObject zigzagSawPrefab;
    [SerializeField] GameObject ghostSawPrefab;

    [Header("Spawn Config")]
    [SerializeField] float spawnRate = 1f;
    [SerializeField] Transform minX;
    [SerializeField] Transform maxX;
    [SerializeField] int poolSize = 20;
    [SerializeField] float difficultyRampUpTime = 60f;

    private Coroutine spawnCoroutine;
    private List<GameObject> obstaclePool = new List<GameObject>();
    private float timeElapsed;

    private void Start()
    {
        InitializePool();
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = GetRandomPrefab();
            GameObject obj = Instantiate(prefab, transform); 
            obj.SetActive(false);
            obstaclePool.Add(obj);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            timeElapsed += spawnRate;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle = GetPooledObject();
        if (obstacle == null)
        {
            GameObject prefab = GetRandomPrefab();
            obstacle = Instantiate(prefab, transform);
            obstacle.SetActive(false);
            obstaclePool.Add(obstacle);
        }

        // Reemplazar el prefab si este era otro tipo
        ReplaceWithCorrectType(ref obstacle);

        Vector3 spawnPosition = new Vector3(
            Random.Range(minX.position.x, maxX.position.x),
            transform.position.y,
            transform.position.z
        );

        obstacle.transform.position = spawnPosition;
        obstacle.transform.rotation = Quaternion.identity;
        obstacle.SetActive(true);
    }

    GameObject GetPooledObject()
    {
        foreach (var obj in obstaclePool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }
        return null;
    }

    void ReplaceWithCorrectType(ref GameObject obj)
    {
        GameObject newPrefab = GetRandomPrefab();
        string currentName = obj.name.Replace("(Clone)", "");

        if (newPrefab.name != currentName)
        {
            int index = obstaclePool.IndexOf(obj);
            Destroy(obj);
            obj = Instantiate(newPrefab, transform);
            obj.SetActive(false);
            obstaclePool[index] = obj;
        }
    }

    GameObject GetRandomPrefab()
    {
        float t = Mathf.Clamp01(timeElapsed / difficultyRampUpTime);
        float normalWeight = Mathf.Lerp(0.8f, 0.4f, t);
        float zigzagWeight = Mathf.Lerp(0.1f, 0.3f, t);
        float ghostWeight = Mathf.Lerp(0.1f, 0.3f, t);

        float total = normalWeight + zigzagWeight + ghostWeight;
        float rand = Random.Range(0f, total);

        if (rand < normalWeight)
            return normalSawVariants[Random.Range(0, normalSawVariants.Length)];
        else if (rand < normalWeight + zigzagWeight)
            return zigzagSawPrefab;
        else
            return ghostSawPrefab;
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }
}
