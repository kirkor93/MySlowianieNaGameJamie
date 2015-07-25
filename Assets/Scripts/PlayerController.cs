using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerIndexEnum
{
    PlayerOne,
    PlayerTwo,
    PlayerThree,
    PlayerFour
}

public class PlayerController : MonoBehaviour {

    public PlayerIndexEnum PlayerIndex = PlayerIndexEnum.PlayerOne;

    public float SpeedMultiplier = 1.0f;
    public float RotationSpeed = 1.0f;

    public float AttackDelay = 0.0f;

    public float StunDuration = 0.0f;

    public float BasePlayerHP = 0.0f;

    public AudioClip[] Clips;

    private float _playerHP = 0.0f;

    private bool _stunned = false;
    private float _stunTimer = 0.0f;

    private bool _resourceInRange = false;
    private bool _collectPeriod = true;

    private AudioSource _myAudioSource;
    private Animator _myAnimator;
    private Rigidbody _myRigidbody;
    private Resource _currentResource;

    private List<Enemy> _targets;


    public bool ResourceInRange
    {
        get;
        set;
    }

    public bool IsStuned
    {
        get { return _stunned; }
    }

	// Use this for initialization
	void Start () {
        _targets = new List<Enemy>();
        _playerHP = BasePlayerHP;
        _myAudioSource = GetComponent<AudioSource>();
        _myAnimator = GetComponent<Animator>();
        _myRigidbody = GetComponent<Rigidbody>();
        GameManager.Instance.OnGamePeriodChange += OnPeriodChange;

	}

    private void OnPeriodChange()
    {
        if (GameManager.Instance.Period == GamePeriod.Collect)
        {
            _collectPeriod = true;
        }
        else
        {
            _collectPeriod = false;
        }
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
            Vector3 move = InputManager.Instance.GetLeftStick(PlayerIndex);

            if (move != Vector3.zero)
            {
                transform.position += move * Time.deltaTime * SpeedMultiplier;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * RotationSpeed);
                _myAnimator.SetBool("Walk", true);
            }
            else
            {
                _myAnimator.SetBool("Walk", false);
            }
            if (_collectPeriod)
            {
                if (_resourceInRange)
                {
                    if (InputManager.Instance.GetAButton(PlayerIndex))
                    {
                        _currentResource.Collect();
                        _resourceInRange = false;
                        _currentResource = null;
                    }
                }
            }
            else
            {
                if (InputManager.Instance.GetAButton(PlayerIndex) && !_myAnimator.GetBool("AttackEnemy"))
                {
                    Debug.Log("Attack");
                    _myAnimator.SetBool("AttackEnemy", true);
                }
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, GameManager.Instance.RestrictionPointOne.position.x, GameManager.Instance.RestrictionPointTwo.position.x), transform.position.y, Mathf.Clamp(transform.position.z, GameManager.Instance.RestrictionPointOne.position.z, GameManager.Instance.RestrictionPointTwo.position.z));
    }

    public void DecreaseHealth(float value)
    {
        _playerHP -= value;
        _myAudioSource.PlayOneShot(Clips[1]);
        _myAnimator.SetBool("IsGettingHit", true);
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
        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _targets.Add(col.gameObject.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy tmpEnemy = col.gameObject.GetComponent<Enemy>();
            if (tmpEnemy != null && _targets.Contains(tmpEnemy))
            {
                _targets.Remove(tmpEnemy);
            }
        }
    }

    public void AnimationGetHitEvent()
    {
        _myAnimator.SetBool("IsGettingHit", false);
    }

    public void AnimationAttackEvent()
    {
        foreach(Enemy enemy in _targets)
        {
            enemy.DecreaseHealth(5.0f);
        }
        _myAnimator.SetBool("AttackEnemy", false);
    }

    public void AnimationAttackSound()
    {
        _myAudioSource.PlayOneShot(Clips[0]);
    }

}
