using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [SerializeField] float attackDelay;
    [SerializeField] float attackRadius;
    [SerializeField] Projectile projectile;

    private List<Transform> enemies = new List<Transform>();
    private EnemyHealth targetEnemy;
    private float elapsedTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemySpawner.Instance.OnEnemySpawn.AddListener(RegisterEnemy);
        EnemySpawner.Instance.OnEnemyDestroy.AddListener(UnregisterEnemy);

        var _allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject _gameObject in _allEnemies)
        {
            enemies.Add(_gameObject.transform);
        }
        GetNearestEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < attackDelay)
            elapsedTime += Time.deltaTime;
        else
        {
            if (targetEnemy == null)
            {
                GetNearestEnemy();
            }
            else
            {
                Attack();
            }
            elapsedTime = 0;
        }
        if (targetEnemy && (targetEnemy.IsDead ||
            Vector2.Distance(targetEnemy.transform.position, transform.position) > attackRadius))
        {
            targetEnemy = null;
            GetNearestEnemy();
        }
    }

    void Attack()
    {
        GameObject _projectile = Instantiate(projectile.gameObject, transform.position, Quaternion.identity);
        Vector2 _dir = targetEnemy.transform.position - transform.position;
        _projectile.GetComponent<Projectile>().Initialize(attackRadius, 5f, _dir);
    }

    private void GetNearestEnemy()
    {
        float _minDis = attackRadius;
        float _curDis;
        foreach (Transform _t in enemies)
        {
            var _hlth = _t.GetComponent<EnemyHealth>();
            _curDis = Vector2.Distance(_t.position, transform.position);
            if (_curDis < _minDis && !_hlth.IsDead)
            {
                _minDis = _curDis;
                targetEnemy = _hlth;
            }
        }
    }
    private void RegisterEnemy(Transform _enemyTransform)
    {
        enemies.Add(_enemyTransform);
    }

    private void UnregisterEnemy(Transform _enemyTransform)
    {
        enemies.Remove(_enemyTransform);
        if (targetEnemy && targetEnemy.transform == _enemyTransform)
        {
            GetNearestEnemy();
            targetEnemy = null;
        }
    }
}
