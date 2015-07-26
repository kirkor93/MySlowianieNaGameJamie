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
        myTransform.rotation = Quaternion.identity;
        Vector3 v = cameraTransform.position - myTransform.position;
        v.x = v.z = 0.0f;
        myTransform.LookAt(cameraTransform.position - v);
        myTransform.Rotate(Vector3.up * 180.0f);
    }

}