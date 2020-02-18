using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public Pool enemyPool;
    [SerializeField] private int maxEnemyCount = 10;
    [SerializeField] private int timeToSpawnEnemy = 25;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] enemySpawns;
    private Vector3[] enemySpawnPosition;
    private void Start()
    {
        enemyPool = new Pool(enemyPrefab, maxEnemyCount);
        enemySpawnPosition = new Vector3[enemySpawns.Length];
        for (int i = 0; i < enemySpawns.Length; i++)
        {
            enemySpawnPosition[i] = enemySpawns[i].transform.position;
            GameObject.Destroy(enemySpawns[i]);
            var temp = enemyPool.Pop();
            temp.transform.position = enemySpawnPosition[i];
            temp.transform.Rotate(0, Random.Range(-120, 120), 0);
        }
        Debug.Log($"Pool count = {enemyPool._pool.Count}");

    }

    public void OnDead()
    {
        StartCoroutine(newEnemy());
    }

    private IEnumerator newEnemy()
    {
        yield return new WaitForSeconds(timeToSpawnEnemy);
        var temp = enemyPool.Pop();
        if (temp != null)
        {
            temp.transform.position = enemySpawnPosition[Random.Range(0, enemySpawnPosition.Length)];
            temp.transform.Rotate(0, Random.Range(-120, 120), 0);
        }
    }
}
