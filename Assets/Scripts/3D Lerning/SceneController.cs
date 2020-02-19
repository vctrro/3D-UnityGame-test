using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public Pool enemyPool;
    [SerializeField] private int maxEnemyCount = 10;
    [SerializeField] private int timeToSpawnEnemy = 25;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] enemySpawns;
    [SerializeField] public Text monsterCount;
    private Vector3[] enemySpawnPosition;
    private void Awake()
    {
        enemyPool = new Pool(enemyPrefab, maxEnemyCount);
        enemySpawnPosition = new Vector3[enemySpawns.Length];

        for (int i = 0; i < enemySpawns.Length; i++)
        {
            enemySpawnPosition[i] = enemySpawns[i].transform.position;
            GameObject.Destroy(enemySpawns[i]);
            var temp = enemyPool.Pop();
            temp.GetComponent<ReactiveTarget>().OnDead.AddListener(EnemyDead);
            temp.transform.position = enemySpawnPosition[i];
            temp.transform.Rotate(0, Random.Range(-120, 120), 0);
        }
    }

    public void EnemyDead()
    {
        monsterCount.text = (maxEnemyCount - enemyPool.PoolCount).ToString();
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
            monsterCount.text = (maxEnemyCount - enemyPool.PoolCount).ToString();
        }
    }
}
