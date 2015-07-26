﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityHall : MonoBehaviour, IDamagable
{
    public GameObject AButton;
    public AudioClip[] Clips;
    public GameObject Upgrade1;
    public GameObject Upgrade2;
    public GameObject Upgrade3;

    public List<GameObject> GatesUpgrade1;
    public List<GameObject> GatesUpgrade2;
    public List<GameObject> GatesUpgrade3;
    public List<GameObject> WallsUpgrade1;
    public List<GameObject> WallsUpgrade2;
    public List<GameObject> WallsUpgrade3;

    private AudioSource _myAudioSource;
    protected bool _isPlayerClose = false;
    protected List<PlayerController> _playerControllerScripts = new List<PlayerController>();

    protected int _upgradeLevel = 1;

    public float HitPoints
    {
        get { return VillageController.Instance.VillageHP; }
    }

    public float MaxHitPoints
    {
        get { return VillageController.Instance.StartHp; }
    }

    // Use this for initialization

    void Start ()
	{
	    _myAudioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame

    void Update () 
    {
	    if(_isPlayerClose)
        {
            foreach(PlayerController pc in _playerControllerScripts)
            {
                if(InputManager.Instance.GetAButton(pc.PlayerIndex))
                {
                    _upgradeLevel += 1;
                    switch(_upgradeLevel)
                    {
                        case 1:
                            Upgrade(true, false, false);
                            VillageController.Instance.VillageHP = 10.0f;
                            VillageController.Instance.StartHp = 10.0f;
                            break;
                        case 2:
                            Upgrade(false, true, false);
                            VillageController.Instance.VillageHP += 5.0f;
                            VillageController.Instance.StartHp += 5.0f;
                            break;
                        case 3:
                            Upgrade(false, false, true);
                            AButton.SetActive(false);
                            VillageController.Instance.VillageHP += 5.0f;
                            VillageController.Instance.StartHp += 5.0f;
                            break;
                    }
                }
            }
        }
	}

    void Upgrade(bool level1, bool level2, bool level3)
    {
        Upgrade1.SetActive(level1);
        foreach(GameObject go in GatesUpgrade1)
        {
            go.SetActive(level1);
        }
        foreach(GameObject go in WallsUpgrade1)
        {
            go.SetActive(level1);
        }

        Upgrade2.SetActive(level2);
        foreach (GameObject go in GatesUpgrade2)
        {
            go.SetActive(level2);
        }
        foreach (GameObject go in WallsUpgrade2)
        {
            go.SetActive(level2);
        }

        Upgrade3.SetActive(level3);
        foreach (GameObject go in GatesUpgrade3)
        {
            go.SetActive(level3);
        }
        foreach (GameObject go in WallsUpgrade3)
        {
            go.SetActive(level3);
        }
    }

    public void DecreaseHealth()
    {
        _myAudioSource.PlayOneShot(Clips[0]);
        VillageController.Instance.VillageHP -= 1.0f;
    }

    void OnTriggerEnter(Collider col)
    {
        if(_upgradeLevel < 3 && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _isPlayerClose = true;
            AButton.SetActive(true);
            _playerControllerScripts.Add(col.GetComponent<PlayerController>());
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _playerControllerScripts.Remove(col.GetComponent<PlayerController>());
            _isPlayerClose = _playerControllerScripts.Count != 0;
            if(!_isPlayerClose)
            {
                AButton.SetActive(false);
            }
        }
    }
}
