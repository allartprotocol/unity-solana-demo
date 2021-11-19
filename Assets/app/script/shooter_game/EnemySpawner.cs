using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Collider2D spawnBounds;
    public Pool<Enemies> enemyPool;

    public GameObject[] enemyPrefabs;
    private Coroutine coroutine;

    public void Start()
    {
        enemyPool = new Pool<Enemies>(enemyPrefabs, 30);
    }

    public void StartSpawn() {
        coroutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawner() {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnRoutine() {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void SpawnEnemy() {
        Enemies enemy = enemyPool.GetFromPool();
        enemy.gameObject.transform.parent = transform;
        Vector3 spawnPosition = RandomPointInBounds(spawnBounds.bounds);
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }
}
