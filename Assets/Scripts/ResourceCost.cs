using UnityEngine;
using System.Collections;

public class ResourceCost {

    public int FoodCost
    {
        get;
        set;
    }
    public int StoneCost
    {
        get;
        set;
    }
    public int IronCost
    {
        get;
        set;
    }
    public int WoodCost
    {
        get;
        set;
    }

    public ResourceCost()
    {
        FoodCost = 0;
        StoneCost = 0;
        IronCost = 0;
        WoodCost = 0;
    }

    public ResourceCost(int food, int stone, int iron, int wood)
    {
        FoodCost = food;
        StoneCost = stone;
        IronCost = iron;
        WoodCost = wood;
    }
}
