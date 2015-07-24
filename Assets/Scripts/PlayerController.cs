using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float SpeedMultiplier = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += InputManager.Instance.GetLeftStick() * Time.deltaTime * SpeedMultiplier;
	}
}
