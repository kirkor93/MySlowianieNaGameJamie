using UnityEngine;
using System.Collections;

public class BillboardCamera : MonoBehaviour 
{
    public Transform MainCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = MainCamera.position;
        transform.rotation = MainCamera.rotation;
	}
}
