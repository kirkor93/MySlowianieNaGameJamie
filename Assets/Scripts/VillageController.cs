using UnityEngine;
using System.Collections;

public class VillageController : Singleton<VillageController> {

    public float StartHp = 10.0f;

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

    //// Use this for initialization
    //public override void Awake()
    //{
    //    base.Awake();

    //}

    private void Start()
    {
        VillageHP = StartHp;
        StoneNeededValue = 10;
        IronNeededValue = 10;
        WoodNeededValue = 15;
        FoodNeededValue = 15;
    }

    private void OnPeriodChange()
    {
        if(GameManager.Instance.Period == GamePeriod.Collect)
        {
            StoneNeededValue += 5;
            IronNeededValue += 5;
            WoodNeededValue += 5;
            FoodNeededValue += 5;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Delete debug when informations will be on GUI
        //Debug.Log(string.Format("Food {0} / Iron {1} / Stone {2} / Wood {3} /", FoodValue, IronValue, StoneValue, WoodValue));
        if(VillageHP <= 0.0f)
        {
            Debug.LogError("CITY HALL IS DESTROYED!!");
        }
	}

    public void BuyBuilding(BuildingType type, UpgradeLvl lvl)
    {
        switch (type)
        {
            case BuildingType.Tower:
                #region UpgradeValues
                switch (lvl)
                {
                    case UpgradeLvl.L1:
                        if (FoodValue >= 2 && StoneValue >= 1 && IronValue >= 0 && WoodValue >= 3)
                        {
                            FoodValue -= 2;
                            StoneValue -= 1;
                            IronValue -= 0;
                            WoodValue -= 3;
                        }
                        break;
                    case UpgradeLvl.L2:
                        if (FoodValue >= 2 && StoneValue >= 4 && IronValue >= 1 && WoodValue >= 2)
                        {
                            FoodValue -= 2;
                            StoneValue -= 4;
                            IronValue -= 1;
                            WoodValue -= 2;
                        }
                        break;
                    case UpgradeLvl.L3:
                        if (FoodValue >= 2 && StoneValue >= 2 && IronValue >= 3 && WoodValue >= 2)
                        {
                            FoodValue -= 2;
                            StoneValue -= 2;
                            IronValue -= 3;
                            WoodValue -= 2;
                        }
                        break;
                }
                break;
                #endregion
            case BuildingType.Mortar:
                #region UpgradeValues
                switch (lvl)
                {
                    case UpgradeLvl.L1:
                        if (FoodValue >= 2 && StoneValue >= 0 && IronValue >= 4 && WoodValue >= 3)
                        {
                            FoodValue -= 2;
                            StoneValue -= 0;
                            IronValue -= 4;
                            WoodValue -= 3;
                        }
                        break;
                    case UpgradeLvl.L2:
                        if (FoodValue >= 2 && StoneValue >= 4 && IronValue >= 2 && WoodValue >= 2)
                        {
                            FoodValue -= 2;
                            StoneValue -= 4;
                            IronValue -= 2;
                            WoodValue -= 2;
                        }
                        break;
                    case UpgradeLvl.L3:
                        if (FoodValue >= 2 && StoneValue >= 2 && IronValue >= 5 && WoodValue >= 3)
                        {
                            FoodValue -= 2;
                            StoneValue -= 2;
                            IronValue -= 5;
                            WoodValue -= 3;
                        }
                        break;
                }
                break;
                #endregion
            case BuildingType.Cannon:
                #region UpgradeValues
                switch (lvl)
                {
                    case UpgradeLvl.L1:
                        if (FoodValue >= 2 && StoneValue >= 1 && IronValue >= 3 && WoodValue >= 3)
                        {
                            FoodValue -= 2;
                            StoneValue -= 1;
                            IronValue -= 3;
                            WoodValue -= 3;
                        }
                        break;
                    case UpgradeLvl.L2:
                        if (FoodValue >= 2 && StoneValue >= 5 && IronValue >= 3 && WoodValue >= 1)
                        {
                            FoodValue -= 2;
                            StoneValue -= 5;
                            IronValue -= 3;
                            WoodValue -= 1;
                        }
                        break;
                    case UpgradeLvl.L3:
                        if (FoodValue >= 2 && StoneValue >= 3 && IronValue >= 4 && WoodValue >= 2)
                        {
                            FoodValue -= 2;
                            StoneValue -= 3;
                            IronValue -= 4;
                            WoodValue -= 2;
                        }
                        break;
                }
                break;
                #endregion
            case BuildingType.Village:
                #region UpgradeValues
                switch (lvl)
                {
                    case UpgradeLvl.L1:
                        if (FoodValue >= 5 && StoneValue >= 2 && IronValue >= 2 && WoodValue >= 6)
                        {
                            FoodValue -= 5;
                            StoneValue -= 2;
                            IronValue -= 2;
                            WoodValue -= 6;
                        }
                        break;
                    case UpgradeLvl.L2:
                        if (FoodValue >= 4 && StoneValue >= 6 && IronValue >= 2 && WoodValue >= 3)
                        {
                            FoodValue -= 4;
                            StoneValue -= 6;
                            IronValue -= 2;
                            WoodValue -= 3;
                        }
                        break;
                    case UpgradeLvl.L3:
                        if (FoodValue >= 5 && StoneValue >= 3 && IronValue >= 6 && WoodValue >= 2)
                        {
                            FoodValue -= 5;
                            StoneValue -= 3;
                            IronValue -= 6;
                            WoodValue -= 2;
                        }
                        break;
                }
                break;
                #endregion
        }
    }

    public bool CanUpgradeBuilding(BuildingType type, UpgradeLvl lvl)
    {
        switch(type)
        {
            case BuildingType.Tower:
                #region UpgradeValues
                switch (lvl)
                {
                    case UpgradeLvl.L1:
                        if(FoodValue >= 2 && StoneValue >= 1 && IronValue >= 0 && WoodValue >= 3)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L2:
                        if (FoodValue >= 2 && StoneValue >= 4 && IronValue >= 1 && WoodValue >= 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L3:
                        if (FoodValue >= 2 && StoneValue >= 2 && IronValue >= 3 && WoodValue >= 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    default:
                        return false;
                }
                #endregion
            case BuildingType.Mortar:
                #region UpgradeValues
                switch (lvl)
                {
                    case UpgradeLvl.L1:
                        if (FoodValue >= 2 && StoneValue >= 0 && IronValue >= 4 && WoodValue >= 3)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L2:
                        if (FoodValue >= 2 && StoneValue >= 4 && IronValue >= 2 && WoodValue >= 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L3:
                        if (FoodValue >= 2 && StoneValue >= 2 && IronValue >= 5 && WoodValue >= 3)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    default:
                        return false;
                }
                #endregion
            case BuildingType.Cannon:
                #region UpgradeValues
                switch (lvl)
                {
                    case UpgradeLvl.L1:
                        if (FoodValue >= 2 && StoneValue >= 1 && IronValue >= 3 && WoodValue >= 3)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L2:
                        if (FoodValue >= 2 && StoneValue >= 5 && IronValue >= 3 && WoodValue >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L3:
                        if (FoodValue >= 2 && StoneValue >= 3 && IronValue >= 4 && WoodValue >= 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    default:
                        return false;
                }
                #endregion
            case BuildingType.Village:
                #region UpgradeValues
                switch (lvl)
                {
                    case UpgradeLvl.L1:
                        if (FoodValue >= 5 && StoneValue >= 2 && IronValue >= 2 && WoodValue >= 6)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L2:
                        if (FoodValue >= 4 && StoneValue >= 6 && IronValue >= 2 && WoodValue >= 3)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L3:
                        if (FoodValue >= 5 && StoneValue >= 3 && IronValue >= 6 && WoodValue >= 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    default:
                        return false;
                }
                #endregion
            default:
                return false;
        }
    }
}

public enum BuildingType
{
    Tower,
    Cannon,
    Mortar,
    Village
}

public enum UpgradeLvl
{
    L1,
    L2,
    L3
}
