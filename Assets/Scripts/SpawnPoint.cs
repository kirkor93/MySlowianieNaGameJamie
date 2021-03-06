﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour 
{
    public List<GameObject> Enemies;
    public float SpawnCooldown = 10.0f;
//    public int StartRound = 1;

    protected bool _spawnEnemies;
    protected int _enemiesCount = 0;
    protected int _lastEnemyIndex = 0;
    protected float _timer;

    void OnEnable()
    {
        GameManager.Instance.OnGamePeriodChange += OnGamePeriodChange;
        _enemiesCount = Enemies.Count;
//        SpawnCooldown = Random.Range(0.1f, 0.5f);
    }

    void Update()
    {
        if(_spawnEnemies)
        {
            _timer += Time.deltaTime;
            if(_timer >= SpawnCooldown)
            {
//                SpawnCooldown = Random.Range(2.0f, 5.0f);
                Enemies[_lastEnemyIndex].SetActive(true);
                ++_lastEnemyIndex;
                --_enemiesCount;
                _timer = 0.0f;
                if(_enemiesCount <= 0)
                {
                    _spawnEnemies = false;
                    _timer = 0.0f;
                    _lastEnemyIndex = 0;
                    _enemiesCount = Enemies.Count;
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
        _timer = 0.0f;
        _lastEnemyIndex = 0;
        _enemiesCount = Enemies.Count;
        if(GameManager.Instance.Period == GamePeriod.Collect)
        {
            foreach(GameObject enemy in Enemies)
            {
                enemy.transform.position = transform.position;
                enemy.transform.rotation = transform.rotation;
            }
            float rand = Random.Range(0.0f, 1.0f);
            if (rand > 0.5f
                && SpawnCooldown > 2)
            {
                SpawnCooldown -= 0.5f;
            }
        }
    }
}
