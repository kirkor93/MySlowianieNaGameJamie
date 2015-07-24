using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float SpeedMultiplier = 1.0f;

    private bool _resourceInRange = false;

    private Resource _currentResource;

    public bool ResourceInRange
    {
        get;
        set;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += InputManager.Instance.GetLeftStick() * Time.deltaTime * SpeedMultiplier;
	    if(_resourceInRange)
        {
            if(InputManager.Instance.GetAButton())
            {
                _currentResource.Collect();
            }
        }
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
