using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;

    [SerializeField] float spawnRate = 1f;
    //[SerializeField] float spawnRateIncrease = 0.1f;

    [SerializeField] Transform minX;
    [SerializeField] Transform maxX;

    Coroutine spawnCoroutine;

    private void Start()
    {
        StartSpawning();
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
            //if(spawnRate > 0.3f)
            //spawnRate -= spawnRateIncrease;
        }
    }

    void Spawn()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minX.position.x, maxX.position.x), transform.position.y, transform.position.z);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnCoroutine);
    }
}
