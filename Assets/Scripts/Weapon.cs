using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public CharacterType MyType;

    public bool AttackDetecion
    {
        get;
        set;
    }

	// Use this for initialization
	void Start () {
        AttackDetecion = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        switch(MyType)
        {
            case CharacterType.Viking:
                if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    col.gameObject.GetComponent<Enemy>();
                }
                break;
            case CharacterType.Crusader:
                if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    col.gameObject.GetComponent<PlayerController>();
                }
                break;
        }
    }
}

public enum CharacterType
{
    Viking,
    Crusader
}
