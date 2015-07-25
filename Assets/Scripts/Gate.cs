using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour 
{
    protected float _hp;

    public bool IsDestroyed { get; protected set; }

    void OnEnable()
    {
        _hp = 500.0f;
        IsDestroyed = false;
    }

    public void DecreaseHealth(float dmg)
    {
        _hp -= dmg;
        if(_hp <= 0.0f)
        {
            IsDestroyed = true;
        }
    }
}
