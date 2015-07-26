using System;
using UnityEngine;
using System.Collections;

public class VillageController : Singleton<VillageController> {

    public float StartHp = 10.0f;

    #region ResourcesCosts
    public ResourceCost TowerLvl1 = new ResourceCost(2, 1, 0, 3);
    public ResourceCost TowerLvl2 = new ResourceCost(2, 4, 1, 2);
    public ResourceCost TowerLvl3 = new ResourceCost(2, 2, 3, 2);
    public ResourceCost MortarLvl1 = new ResourceCost(2, 0, 4, 3);
    public ResourceCost MortarLvl2 = new ResourceCost(2, 4, 2, 2);
    public ResourceCost MortarLvl3 = new ResourceCost(2, 2, 5, 3);
    public ResourceCost CannonLvl1 = new ResourceCost(2, 1, 3, 3);
    public ResourceCost CannonLvl2 = new ResourceCost(2, 5, 3, 1);
    public ResourceCost CannonLvl3 = new ResourceCost(2, 3, 4, 2);
    public ResourceCost VillageLvl1 = new ResourceCost(5, 2, 2, 6);
    public ResourceCost VillageLvl2 = new ResourceCost(4, 6, 2, 3);
    public ResourceCost VillageLvl3 = new ResourceCost(5, 3, 6, 2);

    public event EventHandler<EventArgs> OnGameOver; 
    #endregion

    #region VillageHP and ResourcesValues Properties
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
    #endregion

    #region ResourcesNeededValue
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
    #endregion


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

    void OnEnable()
    {
        Invoke("CalculateCosts", 0.3f);
    }

    void CalculateCosts()
    {
        int activePlayers = 0;
        if(GameManager.Instance.PlayerFour.activeSelf)
        {
            ++activePlayers;
        }
        if(GameManager.Instance.PlayerOne.activeSelf)
        {
            ++activePlayers;
        }
        if (GameManager.Instance.PlayerThree.activeSelf)
        {
            ++activePlayers;
        }
        if (GameManager.Instance.PlayerTwo.activeSelf)
        {
            ++activePlayers;
        }

        Cost(TowerLvl1, activePlayers);
        Cost(TowerLvl2, activePlayers);
        Cost(TowerLvl3, activePlayers);
        Cost(CannonLvl1, activePlayers);
        Cost(CannonLvl2, activePlayers);
        Cost(CannonLvl3, activePlayers);
        Cost(MortarLvl1, activePlayers);
        Cost(MortarLvl2, activePlayers);
        Cost(MortarLvl3, activePlayers);
        Cost(VillageLvl1, activePlayers);
        Cost(VillageLvl2, activePlayers);
        Cost(VillageLvl3, activePlayers);
    }

    void Cost(ResourceCost resource, int multiplier)
    {
        resource.FoodCost *= multiplier;
        resource.IronCost *= multiplier;
        resource.StoneCost *= multiplier;
        resource.WoodCost *= multiplier;
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
//            Debug.LogError("CITY HALL IS DESTROYED!!");
            if (OnGameOver != null)
            {
                OnGameOver(this, EventArgs.Empty);
            }
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
                        if (FoodValue >= TowerLvl1.FoodCost && StoneValue >= TowerLvl1.StoneCost && IronValue >= TowerLvl1.IronCost && WoodValue >= TowerLvl1.WoodCost)
                        {
                            FoodValue -= TowerLvl1.FoodCost;
                            StoneValue -= TowerLvl1.StoneCost;
                            IronValue -= TowerLvl1.IronCost;
                            WoodValue -= TowerLvl1.WoodCost;
                        }
                        break;
                    case UpgradeLvl.L2:
                        if (FoodValue >= TowerLvl2.FoodCost && StoneValue >= TowerLvl2.StoneCost && IronValue >= TowerLvl2.IronCost && WoodValue >= TowerLvl2.WoodCost)
                        {
                            FoodValue -= TowerLvl2.FoodCost;
                            StoneValue -= TowerLvl2.StoneCost;
                            IronValue -= TowerLvl2.IronCost;
                            WoodValue -= TowerLvl2.WoodCost;
                        }
                        break;
                    case UpgradeLvl.L3:
                        if (FoodValue >= TowerLvl3.FoodCost && StoneValue >= TowerLvl3.StoneCost && IronValue >= TowerLvl3.IronCost && WoodValue >= TowerLvl3.WoodCost)
                        {
                            FoodValue -= TowerLvl3.FoodCost;
                            StoneValue -= TowerLvl3.StoneCost;
                            IronValue -= TowerLvl3.IronCost;
                            WoodValue -= TowerLvl3.WoodCost;
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
                        if (FoodValue >= MortarLvl1.FoodCost && StoneValue >= MortarLvl1.StoneCost && IronValue >= MortarLvl1.IronCost && WoodValue >= MortarLvl1.WoodCost)
                        {
                            FoodValue -= MortarLvl1.FoodCost;
                            StoneValue -= MortarLvl1.StoneCost;
                            IronValue -= MortarLvl1.IronCost;
                            WoodValue -= MortarLvl1.WoodCost;
                        }
                        break;
                    case UpgradeLvl.L2:
                        if (FoodValue >= MortarLvl2.FoodCost && StoneValue >= MortarLvl2.StoneCost && IronValue >= MortarLvl2.IronCost && WoodValue >= MortarLvl2.WoodCost)
                        {
                            FoodValue -= MortarLvl2.FoodCost;
                            StoneValue -= MortarLvl2.StoneCost;
                            IronValue -= MortarLvl2.IronCost;
                            WoodValue -= MortarLvl2.WoodCost;
                        }
                        break;
                    case UpgradeLvl.L3:
                        if (FoodValue >= MortarLvl3.FoodCost && StoneValue >= MortarLvl3.StoneCost && IronValue >= MortarLvl3.IronCost && WoodValue >= MortarLvl3.WoodCost)
                        {
                            FoodValue -= MortarLvl3.FoodCost;
                            StoneValue -= MortarLvl3.StoneCost;
                            IronValue -= MortarLvl3.IronCost;
                            WoodValue -= MortarLvl3.WoodCost;
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
                        if (FoodValue >= CannonLvl1.FoodCost && StoneValue >= CannonLvl1.StoneCost && IronValue >= CannonLvl1.IronCost && WoodValue >= CannonLvl1.WoodCost)
                        {
                            FoodValue -= CannonLvl1.FoodCost;
                            StoneValue -= CannonLvl1.StoneCost;
                            IronValue -= CannonLvl1.IronCost;
                            WoodValue -= CannonLvl1.WoodCost;
                        }
                        break;
                    case UpgradeLvl.L2:
                        if (FoodValue >= CannonLvl2.FoodCost && StoneValue >= CannonLvl2.StoneCost && IronValue >= CannonLvl2.IronCost && WoodValue >= CannonLvl2.WoodCost)
                        {
                            FoodValue -= CannonLvl2.FoodCost;
                            StoneValue -= CannonLvl2.StoneCost;
                            IronValue -= CannonLvl2.IronCost;
                            WoodValue -= CannonLvl2.WoodCost;
                        }
                        break;
                    case UpgradeLvl.L3:
                        if (FoodValue >= CannonLvl3.FoodCost && StoneValue >= CannonLvl3.StoneCost && IronValue >= CannonLvl3.IronCost && WoodValue >= CannonLvl3.WoodCost)
                        {
                            FoodValue -= CannonLvl3.FoodCost;
                            StoneValue -= CannonLvl3.StoneCost;
                            IronValue -= CannonLvl3.IronCost;
                            WoodValue -= CannonLvl3.WoodCost;
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
                        if (FoodValue >= VillageLvl1.FoodCost && StoneValue >= VillageLvl1.StoneCost && IronValue >= VillageLvl1.IronCost && WoodValue >= VillageLvl1.WoodCost)
                        {
                            FoodValue -= VillageLvl1.FoodCost;
                            StoneValue -= VillageLvl1.StoneCost;
                            IronValue -= VillageLvl1.IronCost;
                            WoodValue -= VillageLvl1.WoodCost;
                        }
                        break;
                    case UpgradeLvl.L2:
                        if (FoodValue >= VillageLvl2.FoodCost && StoneValue >= VillageLvl2.StoneCost && IronValue >= VillageLvl2.IronCost && WoodValue >= VillageLvl2.WoodCost)
                        {
                            FoodValue -= VillageLvl2.FoodCost;
                            StoneValue -= VillageLvl2.StoneCost;
                            IronValue -= VillageLvl2.IronCost;
                            WoodValue -= VillageLvl2.WoodCost;
                        }
                        break;
                    case UpgradeLvl.L3:
                        if (FoodValue >= VillageLvl3.FoodCost && StoneValue >= VillageLvl3.StoneCost && IronValue >= VillageLvl3.IronCost && WoodValue >= VillageLvl3.WoodCost)
                        {
                            FoodValue -= VillageLvl3.FoodCost;
                            StoneValue -= VillageLvl3.StoneCost;
                            IronValue -= VillageLvl3.IronCost;
                            WoodValue -= VillageLvl3.WoodCost;
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
                        if (FoodValue >= TowerLvl1.FoodCost && StoneValue >= TowerLvl1.StoneCost && IronValue >= TowerLvl1.IronCost && WoodValue >= TowerLvl1.WoodCost)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L2:
                        if (FoodValue >= TowerLvl2.FoodCost && StoneValue >= TowerLvl2.StoneCost && IronValue >= TowerLvl2.IronCost && WoodValue >= TowerLvl2.WoodCost)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L3:
                        if (FoodValue >= TowerLvl3.FoodCost && StoneValue >= TowerLvl3.StoneCost && IronValue >= TowerLvl3.IronCost && WoodValue >= TowerLvl3.WoodCost)
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
                        if (FoodValue >= MortarLvl1.FoodCost && StoneValue >= MortarLvl1.StoneCost && IronValue >= MortarLvl1.IronCost && WoodValue >= MortarLvl1.WoodCost)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L2:
                        if (FoodValue >= MortarLvl2.FoodCost && StoneValue >= MortarLvl2.StoneCost && IronValue >= MortarLvl2.IronCost && WoodValue >= MortarLvl2.WoodCost)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L3:
                        if (FoodValue >= MortarLvl3.FoodCost && StoneValue >= MortarLvl3.StoneCost && IronValue >= MortarLvl3.IronCost && WoodValue >= MortarLvl3.WoodCost)
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
                        if (FoodValue >= CannonLvl1.FoodCost && StoneValue >= CannonLvl1.StoneCost && IronValue >= CannonLvl1.IronCost && WoodValue >= CannonLvl1.WoodCost)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L2:
                        if (FoodValue >= CannonLvl2.FoodCost && StoneValue >= CannonLvl2.StoneCost && IronValue >= CannonLvl2.IronCost && WoodValue >= CannonLvl2.WoodCost)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L3:
                        if (FoodValue >= CannonLvl3.FoodCost && StoneValue >= CannonLvl3.StoneCost && IronValue >= CannonLvl3.IronCost && WoodValue >= CannonLvl3.WoodCost)
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
                        if (FoodValue >= VillageLvl1.FoodCost && StoneValue >= VillageLvl1.StoneCost && IronValue >= VillageLvl1.IronCost && WoodValue >= VillageLvl1.WoodCost)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L2:
                        if (FoodValue >= VillageLvl2.FoodCost && StoneValue >= VillageLvl2.StoneCost && IronValue >= VillageLvl2.IronCost && WoodValue >= VillageLvl2.WoodCost)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case UpgradeLvl.L3:
                        if (FoodValue >= VillageLvl3.FoodCost && StoneValue >= VillageLvl3.StoneCost && IronValue >= VillageLvl3.IronCost && WoodValue >= VillageLvl3.WoodCost)
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
