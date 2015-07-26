using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    public float Speed = 1.0f;

    protected GameObject _target;
    protected Enemy _targetEnemyScript;
    protected float _damage;

    protected Vector3 _initPosition;
    protected float _timer = 0.0f;

    private ParticleSystem _particle;

    // Use this for initialization
    protected virtual void Start()
    {
        _initPosition = transform.position;
        OnPositionsSet();
    }

    void OnEnable()
    {
        _timer = 0.0f;
        _initPosition = transform.position;
        OnPositionsSet();
    }

    protected virtual void OnPositionsSet()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_targetEnemyScript.IsDead)
        {
            Debug.Log("But enemy is dead");
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.Lerp(_initPosition, _target.transform.position, _timer);
            _timer += Speed * Time.deltaTime;
            if (_timer >= 1.0f)
            {
                Debug.Log("Attack succesfull");
                _targetEnemyScript.DecreaseHealth(_damage);
                OnDamage();
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnDamage()
    {
        if (_particle == null)
        {
            return;
        }
        _particle.transform.parent = null;
        _particle.enableEmission = true;
        _particle.Emit(1);
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
