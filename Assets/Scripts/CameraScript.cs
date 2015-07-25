using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
    public Transform[] Targets;
    public float MinimalDistance = 15.0f;
    public float CameraMovingSpeed = 0.05f;

    protected Vector3 _sum;
    protected int _activePlayers;
    protected Vector3 _farest;
    protected Camera _camera;

    void Awake()
    {
        Invoke("CalculatePlayers", 0.5f);
        _camera = GetComponent<Camera>();
    }

    void CalculatePlayers()
    {
        foreach (Transform t in Targets)
        {
            if (t.gameObject.activeSelf)
            {
                ++_activePlayers;
            }
        }
    }

    void Update()
    {
        _sum = Vector3.zero;
        foreach(Transform t in Targets)
        {
            if (t.gameObject.activeSelf)
            {
                _sum += t.position;
            }
        }
        Vector3 center = _sum / _activePlayers;
        float farestDistance = 0.0f;
        foreach(Transform t in Targets)
        {
            if(t.gameObject.activeSelf)
            {
                float dist = Vector3.Distance(t.position, center);
                if (dist > farestDistance)
                {
                    farestDistance = dist;
                    _farest = t.position;
                }
            }
        }
        Vector3 backVector = (Vector3.up + Vector3.back) * (MinimalDistance + farestDistance);
        backVector = Quaternion.AngleAxis(transform.rotation.x, Vector3.right) * backVector;
        transform.position = Vector3.Lerp(transform.position, center + backVector, CameraMovingSpeed);
    }
}
