using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float SpeedMultiplier = 1.0f;

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
        _myRigidbody = GetComponent<Rigidbody>();
        _weapon = transform.GetChild(0).gameObject;
        GameManager.Instance.OnGamePeriodChange += OnPeriodChange;
	}

    private void OnPeriodChange()
    {
        _collectPeriod = !_collectPeriod;
    }
	
	// Update is called once per frame
	void Update () {

        _myRigidbody.velocity = Vector3.zero;

        transform.position += InputManager.Instance.GetLeftStick() * Time.deltaTime * SpeedMultiplier;
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
            if(InputManager.Instance.GetAButton())
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
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


}
