using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
    enum SpawnPosition
    {
        
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private IEnumerator newEnemy()
    {
        yield return new WaitForSeconds(10);

    }
}
