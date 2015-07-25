using UnityEngine;
using System.Collections;

public class CityHall : MonoBehaviour, IDamagable
{

    public AudioClip[] Clips;

    private AudioSource _myAudioSource;

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

    void Update () {
	
	}

    public void DecreaseHealth()
    {
        _myAudioSource.PlayOneShot(Clips[0]);
        Debug.Log("Oops, enemy");
        VillageController.Instance.VillageHP -= 1.0f;
    }
}
