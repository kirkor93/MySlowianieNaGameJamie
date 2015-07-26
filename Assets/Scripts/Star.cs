using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    public float RotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.down, RotationSpeed * Time.deltaTime);    
	}
}
