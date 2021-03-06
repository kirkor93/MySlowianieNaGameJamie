﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MortarBall : Bullet
{
    protected Vector3 _middlePosition;
    protected bool _middlePositionReached = false;

    protected List<Enemy> _targetEnemies = new List<Enemy>();

    protected override void OnPositionsSet()
    {
        if (_target == null) return;
        _middlePosition = (_initPosition + _target.transform.position) / 2.0f;
        _middlePosition.y += 3.0f;
        _middlePositionReached = false;
    }

    protected override void Update()
    {
        if (_targetEnemyScript.IsDead)
        {
            Destroy(gameObject);
        }
        else
        {
            _timer += Time.deltaTime;
            if(!_middlePositionReached)
            {
                transform.position = Vector3.Lerp(_initPosition, _middlePosition, _timer);
                if(_timer >= 1.0f)
                {
                    _timer = 0.0f;
                    _middlePositionReached = true;
                }
            }
            else
            {
                transform.position = Vector3.Slerp(_middlePosition, _target.transform.position, _timer);
                if (_timer >= 1.0f)
                {
                    _timer = 0.0f;
                    _middlePositionReached = true;
                    //_targetEnemyScript.DecreaseHealth(_damage);
                    OnDamage();
                    Destroy(gameObject);
                }
            }
        }
    }

    protected override void OnDamage()
    {
        //MEGA HIPER UBER BOOOM!
        foreach(Enemy e in _targetEnemies)
        {
            e.DecreaseHealth(_damage);
        }
        base.OnDamage();
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if(!_targetEnemies.Contains(enemy))
            {
                _targetEnemies.Add(enemy);
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if (_targetEnemies.Contains(enemy))
            {
                _targetEnemies.Remove(enemy);
            }
        }
    }
}