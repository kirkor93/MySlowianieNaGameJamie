using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float SpeedMultiplier = 1.0f;
    public float RotationSpeed = 1.0f;

    public float AttackDelay = 0.0f;

    public float StunDuration = 0.0f;

    public float BasePlayerHP = 0.0f;


    private float _playerHP = 0.0f;

    private bool _stunned = false;
    private float _stunTimer = 0.0f;
    private bool _attackFlag = false;
    private float _attackTimer = 0.0f;

    private bool _resourceInRange = false;
    private bool _collectPeriod = true;

    private Rigidbody _myRigidbody;
    private Resource _currentResource;

    private GameObject _weapon;

    public bool ResourceInRange
    {
        get;
        set;
    }

	// Use this for initialization
	void Start () {
        _playerHP = BasePlayerHP;
        _myRigidbody = GetComponent<Rigidbody>();
        GameManager.Instance.OnGamePeriodChange += OnPeriodChange;

	}

    private void OnPeriodChange()
    {
        Debug.Log("Period change");
        _collectPeriod = !_collectPeriod;
    }
	
	// Update is called once per frame
	void Update () {

        _myRigidbody.velocity = Vector3.zero;

        if(_stunned)
        {
            _stunTimer += Time.deltaTime;
            if (_stunTimer > StunDuration)
            {
                _playerHP = BasePlayerHP;
                _stunned = false;
            }
        }
        else
        {
            Vector3 move = InputManager.Instance.GetLeftStick();
            transform.position += move * Time.deltaTime * SpeedMultiplier;

            if(move != Vector3.zero)transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(move), Time.deltaTime * RotationSpeed);

            if(_collectPeriod)
            {
	            if(_resourceInRange)
                {
                    if(InputManager.Instance.GetAButton())
                    {
                        _currentResource.Collect();
                        _resourceInRange = false;
                        _currentResource = null;
                    }
                }
            }
            else
            {
                _attackTimer += Time.deltaTime;
            
                if(InputManager.Instance.GetAButton() && _attackTimer > AttackDelay)
                {
                    Debug.Log("Attack");
                    //Play Anim :3
                    _attackFlag = true;
                    _attackTimer = 0.0f;
                }
            }
        }
    }

    public void DecreaseHealth(float value)
    {
        _playerHP -= value;
        if(_playerHP <= 0.0f)
        {
            _stunned = true;
            _stunTimer = 0.0f;
        }
        Mathf.Clamp(_playerHP, 0.0f, float.MaxValue);
    }

    public void SetResourceInRange(Resource res)
    {
        _resourceInRange = true;
        _currentResource = res;
    }

    public void UnsetResourceInRange()
    {
        _resourceInRange = false;
        _currentResource = null;
    }

    void OnTriggerEnter(Collider col)
    {
        if(_attackFlag)
        {
            if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                col.gameObject.GetComponent<Enemy>().DecreaseHealth(5.0f);
            }
            _attackFlag = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (_attackFlag)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                col.gameObject.GetComponent<Enemy>().DecreaseHealth(5.0f);
            }
            _attackFlag = false;
        }
    }


}
