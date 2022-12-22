using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [HideInInspector] public Pool enemyPool;
    [HideInInspector] public UnityEvent OnGameOver;
    [SerializeField] private int maxEnemyCount = 10;
    [SerializeField] private int playerLives = 3;
    [SerializeField] private int timeToSpawnEnemy = 45;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject[] enemySpawns;
    [SerializeField] public Text monsterCount;
    [SerializeField] public Text lifesCount;
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
            playerController.OnHit.AddListener(OnPlayerHit);
        }
    }

    private void OnPlayerHit()
    {
        
        lifesCount.text = (--playerLives).ToString();
        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        OnGameOver.Invoke();
        gameOver.SetActive(true);
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
