using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpPrefabs;
    [SerializeField] float spawnRate = 8f;
    [SerializeField] Transform minX;
    [SerializeField] Transform maxX;
    [SerializeField] int poolSize = 10;

    private List<GameObject> powerUpPool = new List<GameObject>();
    private Coroutine spawnCoroutine;

    void Start()
    {
        InitializePool();
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            powerUpPool.Add(obj);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        GameObject powerUp = GetPooledObject();
        if (powerUp != null)
        {
            // Seleccionar prefab aleatorio y reasignar si es diferente
            GameObject selectedPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

            if (powerUp.name.StartsWith(selectedPrefab.name) == false)
            {
                // Reemplazamos el objeto del pool por uno del tipo correcto
                int index = powerUpPool.IndexOf(powerUp);
                GameObject newObj = Instantiate(selectedPrefab);
                newObj.SetActive(false);
                Destroy(powerUp); // Limpia el anterior
                powerUpPool[index] = newObj;
                powerUp = newObj;
            }

            Vector3 spawnPosition = new Vector3(Random.Range(minX.position.x, maxX.position.x), transform.position.y, transform.position.z);
            powerUp.transform.position = spawnPosition;
            powerUp.transform.rotation = Quaternion.identity;
            powerUp.SetActive(true);
        }
    }

    GameObject GetPooledObject()
    {
        foreach (var obj in powerUpPool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        powerUpPool.Add(newObj);
        return newObj;
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }
}
