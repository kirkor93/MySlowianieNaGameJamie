using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
public class Resource : MonoBehaviour {

    public ResourceType Type;

    private GameObject _aButton;
    private AudioSource _myAudioSource;
    private MeshRenderer _myRenderer;
    private BoxCollider[] _boxColliders;
    private List<GameObject> _players;

    private bool _canCollect = true;

	// Use this for initialization
	void Start () {
        _players = new List<GameObject>();
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
        if(GameManager.Instance.Period == GamePeriod.Collect)
        {
            _canCollect = true;
            _myRenderer.enabled = true;
            foreach (BoxCollider box in _boxColliders)
            {
                box.enabled = true;
            }
        }
        else
        {
            _canCollect = false;
            _aButton.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(_canCollect && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _players.Add(col.gameObject);
            _aButton.SetActive(true);
            col.gameObject.GetComponent<PlayerController>().SetResourceInRange(this);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (_canCollect && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (_players.Contains(col.gameObject)) _players.Remove(col.gameObject);
            if(_players.Count == 0) _aButton.SetActive(false);
            col.gameObject.GetComponent<PlayerController>().UnsetResourceInRange();
        }
    }

    public void Collect()
    {
        _myAudioSource.Play();
        _myRenderer.enabled = false;
        _canCollect = false;
        foreach(BoxCollider box in _boxColliders)
        {
            box.enabled = false;
        }
        _aButton.SetActive(false);

        int resMultiplier = Random.Range(1, 3);

        switch(Type)
        {
            case ResourceType.Food:
                VillageController.Instance.FoodValue += resMultiplier * 1;
                break;
            case ResourceType.Iron:
                VillageController.Instance.IronValue += resMultiplier * 1;
                break;
            case ResourceType.Stone:
                VillageController.Instance.StoneValue += resMultiplier * 1;
                break;
            case ResourceType.Wood:
                VillageController.Instance.WoodValue += resMultiplier * 1;
                break;
        }

        foreach(GameObject go in _players)
        {
            go.GetComponent<PlayerController>().UnsetResourceInRange();
        }
        _players.Clear();
        //GameManager.Instance.OnGamePeriodChange -= OnChangePeriod;   
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
