using UnityEngine;
using System.Collections;

public class CityHall : MonoBehaviour {

    public AudioClip[] Clips;

    private AudioSource _myAudioSource;

	// Use this for initialization
	void Start () {
	
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
