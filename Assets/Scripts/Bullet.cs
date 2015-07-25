using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    protected GameObject _target;
    protected Enemy _targetEnemyScript;
    protected float _damage;

    protected Vector3 _initPosition;
    protected float _timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        _initPosition = transform.position;
        OnPositionsSet();
    }

    protected virtual void OnPositionsSet()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_targetEnemyScript.IsDead)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.Lerp(_initPosition, _target.transform.position, _timer);
            _timer += Time.deltaTime;
            if (_timer >= 1.0f)
            {
                _targetEnemyScript.DecreaseHealth(_damage);
                OnDamage();
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnDamage()
    {

    }

	void SetTarget(GameObject target)
    {
        _target = target;
        _targetEnemyScript = _target.GetComponent<Enemy>();
    }

    void SetDamage(float damage)
    {
        _damage = damage;
    }
}
