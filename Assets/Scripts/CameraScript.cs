using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
    public Transform[] Targets;
    public float MinimalDistance = 15.0f;
    public float CameraMovingSpeed = 0.05f;
    public Transform PositionWhenAttackPeriod;

    protected Vector3 _sum;
    protected int _activePlayers;
    protected Vector3 _farest;
    protected Camera _camera;
    protected Vector3 _center;

    protected bool _isAttack = false;
    protected float _attackTimer = 0.0f;
    protected Vector3 _centerRightBeforeAttack;

    void Awake()
    {
        Invoke("CalculatePlayers", 0.5f);
        StartCoroutine(MyUpdate());
        _camera = GetComponent<Camera>();
        GameManager.Instance.OnGamePeriodChange += OnGamePeriodChange;
    }

    void CalculatePlayers()
    {
        foreach (Transform t in Targets)
        {
            if (t.gameObject.activeSelf)
            {
                ++_activePlayers;
            }
        }
    }

    IEnumerator MyUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        while(true)
        {
            if (!_isAttack)
            {
                _sum = Vector3.zero;
                foreach (Transform t in Targets)
                {
                    if (t.gameObject.activeSelf)
                    {
                        _sum += t.position;
                    }
                }
                _center = _sum / _activePlayers;
                float farestDistance = 0.0f;
                foreach (Transform t in Targets)
                {
                    if (t.gameObject.activeSelf)
                    {
                        float dist = Vector3.Distance(t.position, _center);
                        if (dist > farestDistance)
                        {
                            farestDistance = dist;
                            _farest = t.position;
                        }
                    }
                }
                Vector3 backVector = (Vector3.up + Vector3.back) * (MinimalDistance + farestDistance);
                backVector = Quaternion.AngleAxis(transform.rotation.x, Vector3.right) * backVector;
                Debug.Log(transform.position + " " + _center + " " + backVector);
                transform.position = Vector3.Lerp(transform.position, _center + backVector, CameraMovingSpeed);
            }
            else
            {
                _attackTimer += 2.0f * Time.deltaTime;
                transform.position = Vector3.Lerp(_centerRightBeforeAttack, PositionWhenAttackPeriod.position, _attackTimer);
            }
            yield return null;
        }
    }

    void OnGamePeriodChange()
    {
        _isAttack = GameManager.Instance.Period == GamePeriod.Defense;
        if(_isAttack)
        {
            _attackTimer = 0.0f;
            _centerRightBeforeAttack = transform.position;
        }
    }
}
