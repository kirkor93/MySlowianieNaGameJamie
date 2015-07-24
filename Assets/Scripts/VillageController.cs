using UnityEngine;
using System.Collections;

public class VillageController : Singleton<VillageController> {

    public float VillageHP
    {
        get;
        set;
    }

    public int WoodValue
    {
        get;
        set;
    }

    public int FoodValue
    {
        get;
        set;
    }

    public int IronValue
    {
        get;
        set;
    }

    public int StoneValue
    {
        get;
        set;
    }

    public int WoodNeededValue
    {
        get;
        set;
    }

    public int FoodNeededValue
    {
        get;
        set;
    }

    public int IronNeededValue
    {
        get;
        set;
    }

    public int StoneNeededValue
    {
        get;
        set;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Delete debug when informations will be on GUI
        Debug.Log(string.Format("Food {0} / Iron {1} / Stone {2} / Wood {3} /", FoodValue, IronValue, StoneValue, WoodValue));
	}
}
