using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public Pool enemyPool;
    [SerializeField] private int maxEnemyCount = 12;
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
        Debug.Log($"");

    }

    private void Update()
    {
        
    }

    private IEnumerator newEnemy()
    {
        yield return new WaitForSeconds(10);

    }
}
