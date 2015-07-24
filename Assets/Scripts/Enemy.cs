using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour 
{
    protected NavMeshAgent _myAgent;

    protected GameObject _gateTarget;
    protected Gate _targetGateScript;

    protected GameObject _playerTarget;
    protected PlayerController _playerControllerScript;

    protected bool _playerInSight = false;
    protected bool _playerClose = false;

    protected bool _gateInSight = false;
    protected bool _gateClose = false;

    protected float _hp;

    protected float _attackTimer = 0.0f;
    protected float _attackCooldown = 1.0f;

    public bool IsDead { get; protected set; }

	// Use this for initialization
	void Start () 
    {
        _myAgent = GetComponent<NavMeshAgent>();
        if(_myAgent == null)
        {
            Debug.LogError("How the fuck is it possible?!");
        }
        _myAgent.SetDestination(Vector3.zero);
	}

    void OnEnable()
    {
        _hp = 50.0f;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(_gateInSight && _gateTarget != null)
        {
            if(_gateClose)
            {
                StopNavMeshAgent();
                if(_attackTimer >= _attackCooldown)
                {
                    _attackTimer = 0.0f;
                    _targetGateScript.DecreaseHealth(100.0f);
                    if(_targetGateScript.IsDestroyed)
                    {
                        Destroy(_targetGateScript.gameObject);
                        _gateClose = false;
                        _myAgent.SetDestination(Vector3.zero);
                        _gateInSight = false;
                    }
                }
                _attackTimer += Time.deltaTime;
            }
            else
            {
                _myAgent.SetDestination(_gateTarget.transform.position);
                _myAgent.speed = 3.5f;
                if(Vector3.Distance(transform.position, _gateTarget.transform.position) < 2.0f)
                {
                    _myAgent.SetDestination(transform.position);
                    _gateClose = true;
                }
            }
            return;
        }

	    if(_playerInSight && _playerTarget != null)
        {
            if(_playerClose)
            {
                Vector3 lookPos = _playerTarget.transform.position - transform.position;
                StopNavMeshAgent();
                if(_attackTimer >= _attackCooldown)
                {
                    _attackTimer = 0.0f;
                    //Attack player
                }
                _attackTimer += Time.deltaTime;
                if(lookPos.magnitude >= 2.5f)
                {
                    _playerClose = false;
                }
            }
            else
            {
                _myAgent.SetDestination(_playerTarget.transform.position);
                _myAgent.speed = 3.5f;
                if ((_playerTarget.transform.position - transform.position).magnitude < 2.0f)
                {
                    _playerClose = true;
                }
            }
        }
	}

    void StopNavMeshAgent()
    {
        _myAgent.speed = 0.0f;
        _myAgent.velocity = Vector3.zero;
    }

    public void DecreaseHealth(float dmg)
    {
        _hp -= dmg;
        if(_hp <= 0.0f)
        {
            _myAgent.Stop();
            _playerTarget = null;
            _gateTarget = null;
            GetComponent<Collider>().enabled = false;
            _playerInSight = false;
            _playerClose = false;
            _gateInSight = false;
            _gateClose = false;
            IsDead = true;
            Destroy(gameObject, 1.0f);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Gate"))
        {
            _attackTimer = 0.0f;
            _gateTarget = col.gameObject;
            _gateInSight = true;
            _gateClose = false;
            _targetGateScript = _gateTarget.GetComponent<Gate>();
        }
        
        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _attackTimer = 0.0f;
            _playerTarget = col.gameObject;
            _playerInSight = true;
            _playerClose = false;
            _playerControllerScript = _playerTarget.GetComponent<PlayerController>();
        }
        
        if(col.gameObject.layer == LayerMask.NameToLayer("CityHall"))
        {
            //City hall attack
            _attackTimer = 0.0f;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if(_gateTarget != null)
        {
            return;
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Gate"))
        {
            _attackTimer = 0.0f;
            _gateTarget = null;
            _gateInSight = false;
            _gateClose = false;
            _myAgent.SetDestination(Vector3.zero);
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _attackTimer = 0.0f;
            _playerTarget = null;
            _playerInSight = false;
            _playerClose = false;
            _myAgent.SetDestination(Vector3.zero);
        }
    }
}
