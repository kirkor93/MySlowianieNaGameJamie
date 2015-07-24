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
        switch(Type)
        {
            case ResourceType.Food:
                VillageController.Instance.IncreaseFood(1);
                break;
            case ResourceType.Iron:
                VillageController.Instance.IncreaseIron(1);
                break;
            case ResourceType.Stone:
                VillageController.Instance.IncreaseStone(1);
                break;
            case ResourceType.Wood:
                VillageController.Instance.IncreaseWood(1);
                break;
        }
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
