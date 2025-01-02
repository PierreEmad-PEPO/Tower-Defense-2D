using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : Singletone<EnemySpawner>
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] enemiesPrefabs;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int enemiesPerSpawn;
    [SerializeField] private int maxEnemiesOnScreen;

    private int enemiesOnScreen;
    private List<Transform> enemies = new List<Transform>();

    public UnityEvent<Transform> OnEnemySpawn;
    public UnityEvent<Transform> OnEnemyDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            if (enemiesOnScreen < maxEnemiesOnScreen && totalEnemies > 0)
            {
                int _randomIndex = Random.Range(0, enemiesPrefabs.Length);
                var _enemy = Instantiate(enemiesPrefabs[_randomIndex]);
                _enemy.transform.position = spawnPoint.transform.position;
                enemies.Add(_enemy.transform);
                enemiesOnScreen++;
                OnEnemySpawn.Invoke(_enemy.transform);

                yield return new WaitForSeconds(0.5f);
                StartCoroutine(SpawnEnemy());
            }
        }
    }

    public void RemoveEnemyFromScreen(EnemyMovement _enemy)
    {
        enemiesOnScreen--;
        enemies.Remove(_enemy.transform);
        OnEnemyDestroy.Invoke(_enemy.transform);

    }
}
