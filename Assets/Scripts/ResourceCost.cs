using UnityEngine;
using System.Collections;

public struct ResourceCost {

    public int FoodCost;
    public int StoneCost;
    public int IronCost;
    public int WoodCost;

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
