using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityHall : MonoBehaviour, IDamagable
{
    public GameObject AButton;
    public AudioClip[] Clips;
    public GameObject Upgrade1;
    public GameObject Upgrade2;
    public GameObject Upgrade3;

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
                            Upgrade1.SetActive(true);
                            Upgrade2.SetActive(false);
                            Upgrade3.SetActive(false);
                            VillageController.Instance.VillageHP = 10.0f;
                            VillageController.Instance.StartHp = 10.0f;
                            break;
                        case 2:
                            Upgrade1.SetActive(false);
                            Upgrade2.SetActive(true);
                            Upgrade3.SetActive(false);
                            VillageController.Instance.VillageHP += 5.0f;
                            VillageController.Instance.StartHp += 5.0f;
                            break;
                        case 3:
                            Upgrade1.SetActive(false);
                            Upgrade2.SetActive(false);
                            Upgrade3.SetActive(true);
                            AButton.SetActive(false);
                            VillageController.Instance.VillageHP += 5.0f;
                            VillageController.Instance.StartHp += 5.0f;
                            break;
                    }
                }
            }
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
