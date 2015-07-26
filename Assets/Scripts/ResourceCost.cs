using UnityEngine;
using System.Collections;

public class ResourceCost {

    public int FoodCost
    {
        private set;
        public get;
    }
    public int StoneCost
    {
        private set;
        public get;
    }
    public int IronCost
    {
        private set;
        public get;
    }
    public int WoodCost
    {
        private set;
        public get;
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
