using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour 
{
    public List<GameObject> Enemies;

    protected bool _spawnEnemies;
    protected int _enemiesCount = 0;
    protected int _lastEnemyIndex = 0;
    protected float _timer;
    protected float _spawnCooldown = 3.0f;

    void OnEnable()
    {
        GameManager.Instance.OnGamePeriodChange += OnGamePeriodChange;
        _enemiesCount = Enemies.Count;
        _spawnCooldown = Random.Range(2.0f, 5.0f);
    }

    void Update()
    {
        if(_spawnEnemies)
        {
            _timer += Time.deltaTime;
            if(_timer >= _spawnCooldown)
            {
                _spawnCooldown = Random.Range(2.0f, 5.0f);
                Enemies[_lastEnemyIndex].SetActive(true);
                ++_lastEnemyIndex;
                --_enemiesCount;
                --GameManager.Instance.EnemiesCount;
                _timer = 0.0f;
                if(_enemiesCount <= 0)
                {
                    _spawnEnemies = false;
                    _timer = 0.0f;
                    _lastEnemyIndex = 0;
                    _enemiesCount = Enemies.Count;
                    GameManager.Instance.EnemiesCount = _enemiesCount;
                }
            }
        }
    }

    public void SpawnEnemies()
    {
        _spawnEnemies = true;
    }

    void OnGamePeriodChange()
    {
        if(GameManager.Instance.Period == GamePeriod.Collect)
        {
            foreach(GameObject enemy in Enemies)
            {
                enemy.transform.position = transform.position;
                enemy.transform.rotation = transform.rotation;
            }
        }
    }
}
