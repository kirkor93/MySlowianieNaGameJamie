using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

    public ResourceType Type;

    private GameObject _aButton;
    private AudioSource _myAudioSource;
    private MeshRenderer _myRenderer;
    private BoxCollider[] _boxColliders;

    private bool _canCollect = true;

	// Use this for initialization
	void Start () {
        _myAudioSource = GetComponent<AudioSource>();
        _myRenderer = GetComponent<MeshRenderer>();
        if(_myRenderer == null)
        {
            _myRenderer = GetComponentInChildren<MeshRenderer>();
        }
        _boxColliders = GetComponents<BoxCollider>();
        _aButton = transform.GetChild(0).gameObject;
        GameManager.Instance.OnGamePeriodChange += OnChangePeriod;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnChangePeriod()
    {
        _canCollect = GameManager.Instance.Period == GamePeriod.Collect;
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
        _myAudioSource.Play();
        _myRenderer.enabled = false;
        foreach(BoxCollider box in _boxColliders)
        {
            box.enabled = false;
        }
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
        
        //Destroy(this.gameObject);
    }
     
}

public enum ResourceType
{
    Food,
    Stone,
    Wood,
    Iron
}
