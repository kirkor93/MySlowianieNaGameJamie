using UnityEngine;
using System.Collections;

public class VillageController : Singleton<VillageController> {

    private int _woodValue = 0;
    private int _foodValue = 0;
    private int _ironValue = 0;
    private int _stoneValue = 0;

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
        Debug.Log(string.Format("Food {0} / Iron {1} / Stone {2} / Wood {3} /", _foodValue, _ironValue, _stoneValue, _woodValue));
	}

    public void IncreaseWood(int value)
    {
        _woodValue += value;
    }

    public void IncreaseFood(int value)
    {
        _foodValue += value;
    }

    public void IncreaseIron(int value)
    {
        _ironValue += value;
    }

    public void IncreaseStone(int value)
    {
        _stoneValue += value;
    }
}
