using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour 
{
    public float AttackDamage = 10.0f;
    public float GateAttackDamage = 50.0f;
    public float EnemyMaxHp = 50.0f;
    public AudioClip[] Clips;

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

    protected AudioSource _myAudioSource;
    protected Rigidbody _myRigidbody;
    protected Animator _myAnimator;

    protected GameObject _hpBar;

    protected Vector3 _destinationBeforeGetHit;

    public bool IsDead { get; protected set; }

	// Use this for initialization
	void Start () 
    {
        _hpBar = transform.GetChild(0).gameObject;
        _myAudioSource = GetComponent<AudioSource>();
        _myRigidbody = GetComponent<Rigidbody>();
        _myAnimator = GetComponent<Animator>();
        _myAgent = GetComponent<NavMeshAgent>();
        if(_myAgent == null)
        {
            Debug.LogError("How the fuck is it possible?!");
        }
        ResumeNavMeshAgent(Vector3.zero);
	}

    void OnEnable()
    {
        _hp = EnemyMaxHp;
        IsDead = false;
        if (_myAgent == null)
        {
            _myAgent = GetComponent<NavMeshAgent>();
        }
        ResumeNavMeshAgent(Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(_gateInSight && _gateTarget != null)
        {
            if(_gateClose)
            {
                StopNavMeshAgent();
                _myAnimator.SetBool("PlayerClose", true);
                if(_targetGateScript.IsDestroyed)
                {
                    _gateTarget.SetActive(false);
                    _gateClose = false;
                    ResumeNavMeshAgent(Vector3.zero);
                    _gateInSight = false;
                    _myAnimator.SetBool("PlayerClose", false);
                }
            }
            else
            {
                _myAnimator.SetBool("PlayerClose", false);
                ResumeNavMeshAgent(_gateTarget.transform.position);
                if(Vector3.Distance(transform.position, _gateTarget.transform.position) < 2.0f)
                {
                    _myAnimator.SetBool("PlayerClose", true);
                    StopNavMeshAgent();
                    _gateClose = true;
                }
            }
            return;
        }

	    if(_playerInSight && _playerTarget != null)
        {
            if(_playerClose)
            {
                if(_playerControllerScript.IsStuned)
                {
                    _myAnimator.SetBool("PlayerClose", false);
                    _playerClose = false;
                    _playerTarget = null;
                    _playerInSight = false;
                    _playerControllerScript = null;
                    ResumeNavMeshAgent(Vector3.zero);
                    return;
                }
                _myAnimator.SetBool("PlayerClose", true);
                Vector3 lookPos = _playerTarget.transform.position - transform.position;
                StopNavMeshAgent();
                if(lookPos.magnitude >= 2.5f)
                {
                    _myAnimator.SetBool("PlayerClose", false);
                    _playerClose = false;
                }
            }
            else
            {
                _myAnimator.SetBool("PlayerClose", false);
                ResumeNavMeshAgent(_playerTarget.transform.position);
                if ((_playerTarget.transform.position - transform.position).magnitude < 2.0f)
                {
                    _myAnimator.SetBool("PlayerClose", true);
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

    void ResumeNavMeshAgent(Vector3 destination)
    {
        _myAgent.speed = 3.5f;
        _myAgent.SetDestination(destination);
    }

    public void DecreaseHealth(float dmg)
    {
        if (_hp <= 0.0f) return;
        StopNavMeshAgent();
        _myAnimator.SetBool("IsGettingHit", true);
        _hp -= dmg;
        _hpBar.transform.GetChild(0).localScale = new Vector3(_hp / EnemyMaxHp * 0.5f, 0.5f, 0.0f);
        if(_hp <= 0.0f)
        {
            _hpBar.transform.GetChild(0).localScale = new Vector3(0.0f, 0.5f, 0.0f);
            GameManager.Instance.EnemiesCount -= 1;
            _playerTarget = null;
            _gateTarget = null;
            GetComponent<Collider>().enabled = false;
            _playerInSight = false;
            _playerClose = false;
            _gateInSight = false;
            _gateClose = false;
            IsDead = true;
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Gate"))
        {
            _gateTarget = col.gameObject;
            _gateInSight = true;
            _gateClose = false;
            _targetGateScript = _gateTarget.GetComponent<Gate>();
        }
        
        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _playerTarget = col.gameObject;
            _playerInSight = true;
            _playerClose = false;
            _playerControllerScript = _playerTarget.GetComponent<PlayerController>();
            _playerControllerScript.DecreaseHealth(5.0f);
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
            _gateTarget = null;
            _gateInSight = false;
            _gateClose = false;
            _myAgent.SetDestination(Vector3.zero);
            _myAnimator.SetBool("PlayerClose", false);
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _playerTarget = null;
            _playerInSight = false;
            _playerClose = false;
            _myAgent.SetDestination(Vector3.zero);
            _myAnimator.SetBool("PlayerClose", false);
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("CityHall"))
        {
            col.gameObject.GetComponent<CityHall>().DecreaseHealth();
            --GameManager.Instance.EnemiesCount;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void AnimationGetHitEvent()
    {
        _myAudioSource.PlayOneShot(Clips[1]);
        _myAnimator.SetBool("IsGettingHit", false);
        ResumeNavMeshAgent(_destinationBeforeGetHit);
    }

    public void AnimationAttackEvent()
    {
        _myAudioSource.PlayOneShot(Clips[0]);
        if(_gateClose)
        {
            _targetGateScript.DecreaseHealth(GateAttackDamage);
            return;
        }

        if(_playerClose)
        {
            _playerControllerScript.DecreaseHealth(AttackDamage);
        }
    }
}
