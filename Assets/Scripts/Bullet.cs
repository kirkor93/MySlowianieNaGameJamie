using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    protected GameObject _target;
    protected Enemy _targetEnemyScript;
    protected float _damage;

    private Vector3 _initPosition;
    private float _timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        _initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
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
                Debug.Log(_damage);
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
