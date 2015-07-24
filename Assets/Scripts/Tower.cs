using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour 
{
    private float _timer = 0.0f;

    protected GameObject _enemy;

    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _attackCooldown;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _attackCooldown)
        {
            _timer = 0.0f;
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("Try attack");
        if (_enemy == null)
        {
            Debug.Log("Nothing to attack");
            return;
        }
        Debug.Log("Attack");
        //Attack this fucking enemy
    }

    protected void OnTriggerEnter(Collider col)
    {
        if(_enemy != null)
        {
            return;
        }
        else if (col.gameObject.layer == GameManager.Instance.EnemyLayer)
        {
            _enemy = col.gameObject;
        }
    }

    protected void OnTriggerExit(Collider col)
    {
        if (_enemy == null) return;
        if (col.gameObject.GetInstanceID() == _enemy.GetInstanceID())
        {
            _enemy = null;
        }
    }
}