using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
    public Transform cameraTransform;
    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    void Update()
    {
        Vector3 v = cameraTransform.position - myTransform.position;
        v.x = v.z = 0.0f;
        myTransform.LookAt(cameraTransform.position - v);
    }

}