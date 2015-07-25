using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour 
{
    public Transform LeftSide;
    public Transform RightSide;
    public float GateOpenCloseSpeed = 3.0f;
    public float GateOpeningAngle = 80.0f;

    protected float _hp;

    public bool IsDestroyed { get; protected set; }

    protected bool _openGate;
    protected bool _closeGate;

    protected Vector3 _desiredLeftSideRotation;
    protected Vector3 _desiredRightSideRotation;
    protected Vector3 _leftSideInitRotation;
    protected Vector3 _rightSideInitRotation;
    protected float _timer = 0.0f;

    void OnEnable()
    {
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
        if(_openGate)
        {
            _timer += Time.deltaTime * GateOpenCloseSpeed;
            RightSide.transform.eulerAngles = Vector3.Lerp(_rightSideInitRotation, _desiredRightSideRotation, _timer);
            LeftSide.transform.eulerAngles = Vector3.Lerp(_leftSideInitRotation, _desiredLeftSideRotation, _timer);
            if(_timer >= 1.0f)
            {
                _openGate = false;
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
            }
        }
    }

    public void DecreaseHealth(float dmg)
    {
        _hp -= dmg;
        if(_hp <= 0.0f)
        {
            IsDestroyed = true;
        }
    }

    void OnGamePeriodChange()
    {
        _timer = 0.0f;
        if(GameManager.Instance.Period == GamePeriod.Collect)
        {
            _openGate = true;
            _closeGate = false;
        }
        else
        {
            _openGate = false;
            _closeGate = true;
        }
    }
}
