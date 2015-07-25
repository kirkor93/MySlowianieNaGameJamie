using UnityEngine;
using System.Collections;

public class CityHall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DecreaseHealth()
    {
        Debug.Log("Oops, enemy");
        VillageController.Instance.VillageHP -= 1.0f;
    }
}
