using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour 
{
    public ParticleSystem ShootParticle;

    private float _timer = 0.0f;

    protected GameObject _enemy;

    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _attackCooldown;

    public GameObject BulletPrefab;
    public Transform ShootingPosition;

    protected AudioSource _myAudioSource;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    void Update()
    {
        if(_enemy != null && !_enemy.activeSelf)
        {
            _enemy = null;
        }
        _timer += Time.deltaTime;
        if (_timer >= _attackCooldown)
        {
            _timer = 0.0f;
            Attack();
        }
    }

    void OnEnable()
    {
        if (ShootParticle == null)
        {
            ShootParticle = transform.parent.GetComponentInChildren<ParticleSystem>();
            if (ShootParticle == null)
            {
                Debug.LogError("Tower couldn't find shoot particle anywhere");
            }
        }
        ShootParticle.transform.position = ShootingPosition.position + Vector3.up * 0.3f;
        _myAudioSource = GetComponent<AudioSource>();
        _timer = 0.0f;
        _enemy = null;
    }

    public void Attack()
    {
        if (_enemy == null)
        {
            return;
        }
        if (ShootParticle != null)
        {
            ShootParticle.Emit(1);
        }
        GameObject bullet = Instantiate(BulletPrefab, ShootingPosition.position, BulletPrefab.transform.rotation) as GameObject;
        bullet.SendMessage("SetTarget", _enemy);
        bullet.SendMessage("SetDamage", _damage);
        _myAudioSource.Play();
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

    protected void OnTriggerStay(Collider col)
    {
        if(_enemy != null)
        {
            return;
        }
        else if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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