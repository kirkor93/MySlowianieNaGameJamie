using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public Vector3 GetLeftStick()
    {
        return new Vector3(Input.GetAxis("Horizontal2"), 0.0f, Input.GetAxis("Vertical2"));
    }

    public bool GetAButton()
    {
        return Input.GetButtonDown("ActionButton");
    }
}
