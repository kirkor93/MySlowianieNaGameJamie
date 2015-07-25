using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gate : MonoBehaviour 
{
    public GameObject AButton;
    public Transform LeftSide;
    public Transform RightSide;
    public float GateOpenCloseSpeed = 3.0f;
    public float GateOpeningAngle = 80.0f;

    public AudioClip[] Clips;

    protected float _hp;

    public bool IsDestroyed { get; protected set; }

    protected bool _openGate;
    protected bool _closeGate;

    protected Vector3 _desiredLeftSideRotation;
    protected Vector3 _desiredRightSideRotation;
    protected Vector3 _leftSideInitRotation;
    protected Vector3 _rightSideInitRotation;
    protected float _timer = 0.0f;

    protected AudioSource _myAudioSource;

    protected List<PlayerController> _playerControllerScript = new List<PlayerController>();

    void OnEnable()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _hp = 500.0f;
        IsDestroyed = false;
        GameManager.Instance.OnGamePeriodChange += OnGamePeriodChange;
        _leftSideInitRotation = LeftSide.transform.eulerAngles;
        _rightSideInitRotation = RightSide.transform.eulerAngles;
        _desiredLeftSideRotation = LeftSide.transform.eulerAngles - new Vector3(0.0f, GateOpeningAngle, 0.0f);
        _desiredRightSideRotation = RightSide.transform.eulerAngles + new Vector3(0.0f, GateOpeningAngle, 0.0f);
    }

    void Update()
    {
        if(_playerControllerScript.Count > 0)
        {
            foreach(PlayerController pc in _playerControllerScript)
            {
                if(InputManager.Instance.GetAButton(pc.PlayerIndex))
                {
                    AButton.SetActive(false);
                    _hp = 500.0f;
                }
            }
        }
        else if(AButton.activeSelf)
        {
            AButton.SetActive(false);
        }

        if(_openGate)
        {
            _timer += Time.deltaTime * GateOpenCloseSpeed;
            RightSide.transform.eulerAngles = Vector3.Lerp(_rightSideInitRotation, _desiredRightSideRotation, _timer);
            LeftSide.transform.eulerAngles = Vector3.Lerp(_leftSideInitRotation, _desiredLeftSideRotation, _timer);
            if(_timer >= 1.0f)
            {
                _openGate = false;
                GetComponent<BoxCollider>().enabled = false;
            }
        }
        else if(_closeGate)
        {
            _timer += Time.deltaTime * GateOpenCloseSpeed;
            RightSide.transform.eulerAngles = Vector3.Lerp(_desiredRightSideRotation, _rightSideInitRotation, _timer);
            LeftSide.transform.eulerAngles = Vector3.Lerp(_desiredLeftSideRotation, _leftSideInitRotation, _timer);
            if (_timer >= 1.0f)
            {
                _closeGate = false;
                GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void DecreaseHealth(float dmg)
    {
        _hp -= dmg;
        _myAudioSource.volume = 1.0f;
        _myAudioSource.PlayOneShot(Clips[1]);
        if(_hp <= 0.0f)
        {
            IsDestroyed = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void OnGamePeriodChange()
    {
        _timer = 0.0f;
        if(GameManager.Instance.Period == GamePeriod.Collect)
        {
            _openGate = true;
            _closeGate = false;
            _myAudioSource.volume = 1.0f;
            _myAudioSource.PlayOneShot(Clips[0]);
        }
        else
        {
            _openGate = false;
            _closeGate = true;
            _myAudioSource.volume = 1.0f;
            _myAudioSource.PlayOneShot(Clips[0]);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") && _hp < 500.0f)
        {
            _playerControllerScript.Add(col.gameObject.GetComponent<PlayerController>());
            AButton.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _playerControllerScript.Remove(col.gameObject.GetComponent<PlayerController>());
        }
    }
}
