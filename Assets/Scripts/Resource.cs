using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

    public ResourceType Type;

    private GameObject _aButton;

    private bool _canCollect = true;

	// Use this for initialization
	void Start () {
        _aButton = transform.GetChild(0).gameObject;
        GameManager.Instance.OnGamePeriodChange += OnChangePeriod;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnChangePeriod()
    {
        _canCollect = !_canCollect;
        if(!_canCollect)
        {
            _aButton.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(_canCollect && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _aButton.SetActive(true);
            col.gameObject.GetComponent<PlayerController>().SetResourceInRange(this);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (_canCollect && col.gameObject.layer == LayerMask.NameToLayer("Player"))
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
                VillageController.Instance.FoodValue += 1;
                break;
            case ResourceType.Iron:
                VillageController.Instance.IronValue += 1;
                break;
            case ResourceType.Stone:
                VillageController.Instance.StoneValue += 1;
                break;
            case ResourceType.Wood:
                VillageController.Instance.WoodValue += 1;
                break;
        }
        GameManager.Instance.OnGamePeriodChange -= OnChangePeriod;
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
