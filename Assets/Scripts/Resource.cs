using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

    public ResourceType Type;

    private GameObject _aButton;

	// Use this for initialization
	void Start () {
        _aButton = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _aButton.SetActive(true);
            col.gameObject.GetComponent<PlayerController>().SetResourceInRange(this);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _aButton.SetActive(false);
            col.gameObject.GetComponent<PlayerController>().UnsetResourceInRange();
        }
    }

    public void Collect()
    {
        _aButton.SetActive(false);
        //TO DO Increase resources value
        Destroy(this.gameObject);
    }
     
}

public enum ResourceType
{
    Food,
    Stone,
    Wood,
    Iron
}
