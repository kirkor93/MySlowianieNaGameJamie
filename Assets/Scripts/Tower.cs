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

    public GameObject BulletPrefab;
    public Transform ShootingPosition;

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

    void OnEnable()
    {
        _timer = 0.0f;
        _enemy = null;
    }

    public void Attack()
    {
        if (_enemy == null)
        {
            return;
        }
        GameObject bullet = Instantiate(BulletPrefab, ShootingPosition.position, BulletPrefab.transform.rotation) as GameObject;
        bullet.SendMessage("SetTarget", _enemy);
        bullet.SendMessage("SetDamage", _damage);
    }

    protected void OnTriggerEnter(Collider col)
    {
        if(_enemy != null)
        {
            return;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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