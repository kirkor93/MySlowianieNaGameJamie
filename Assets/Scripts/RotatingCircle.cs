﻿using UnityEngine;
using System.Collections;

public class RotatingCircle : MonoBehaviour
{
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.forward, -Time.deltaTime * 360.0f);
	}
}
