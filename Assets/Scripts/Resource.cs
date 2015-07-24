using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

    public ResourceType Type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //TO DO Show A button sprite
            //Delete debug when completed
            Debug.Log("Press A");
            col.gameObject.GetComponent<PlayerController>().SetResourceInRange(this);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().UnsetResourceInRange();
        }
    }

    public void Collect()
    {
        //Delete this debug
        Debug.Log("Collected");
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
