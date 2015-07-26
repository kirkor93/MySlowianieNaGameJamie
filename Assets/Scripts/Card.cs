using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour 
{
    public TextMesh Text;
    public TowerKindEnum TowerKind;

    protected TowerSlot _towerSlot;
    protected List<PlayerIndexEnum> _playerIndex = new List<PlayerIndexEnum>();
    protected BuildingType _buildingType = BuildingType.Tower;
    protected UpgradeLvl _upgradeLevel = UpgradeLvl.L1;

    protected SpriteRenderer _spriteRenderer;

    void Start()
    {
        switch(TowerKind)
        {
            case TowerKindEnum.Tower:
                _buildingType = BuildingType.Tower;
                break;
            case TowerKindEnum.Cannon:
                _buildingType = BuildingType.Cannon;
                break;
            case TowerKindEnum.Mortar:
                _buildingType = BuildingType.Mortar;
                break;
        }
        _upgradeLevel = UpgradeLvl.L1;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(VillageController.Instance.CanUpgradeBuilding(_buildingType, _upgradeLevel))
        {
            for (int i = 0; i < _playerIndex.Count; ++i)
            {
                _spriteRenderer.color = Color.white;
                bool condition = false;
                switch (TowerKind)
                {
                    case TowerKindEnum.Tower:
                        condition = InputManager.Instance.GetBButton(_playerIndex[i]);
                        break;
                    case TowerKindEnum.Cannon:
                        condition = InputManager.Instance.GetYButton(_playerIndex[i]);
                        _upgradeLevel = UpgradeLvl.L3;
                        break;
                    case TowerKindEnum.Mortar:
                        condition = InputManager.Instance.GetXButton(_playerIndex[i]);
                        _upgradeLevel = UpgradeLvl.L3;
                        break;
                }
                if (condition)
                {
                    VillageController.Instance.BuyBuilding(_buildingType, _upgradeLevel);
                    _towerSlot.SetUpgrade(TowerKind);
                }
            }
        }
        else
        {
            _spriteRenderer.color = Color.gray;
        }
    }

    void OnDisable()
    {
        _playerIndex.Clear();
    }

    public void SetTowerSlot(TowerSlot ts)
    {
        _towerSlot = ts;
    }

    public void SetPlayerIndex(PlayerIndexEnum playerIndex)
    {
        if(!_playerIndex.Contains(playerIndex))
        {
            _playerIndex.Add(playerIndex);
        }
    }

    public void RemovePlayerIndex(PlayerIndexEnum playerIndex)
    {
        if(_playerIndex.Contains(playerIndex))
        {
            _playerIndex.Remove(playerIndex);
        }
    }

    public void SetUpgradeLevel(UpgradeLvl upgradeLevel)
    {
        _upgradeLevel = upgradeLevel;
        ResourceCost myResource = null;
        switch(_buildingType)
        {
            case BuildingType.Tower:
                switch(_upgradeLevel)
                {
                    case UpgradeLvl.L1:
                        myResource = VillageController.Instance.TowerLvl1;
                        break;
                    case UpgradeLvl.L2:
                        myResource = VillageController.Instance.TowerLvl2;
                        break;
                    case UpgradeLvl.L3:
                        myResource = VillageController.Instance.TowerLvl3;
                        break;
                }
                break;
            case BuildingType.Cannon:
                switch (_upgradeLevel)
                {
                    case UpgradeLvl.L1:
                        myResource = VillageController.Instance.CannonLvl1;
                        break;
                    case UpgradeLvl.L2:
                        myResource = VillageController.Instance.CannonLvl2;
                        break;
                    case UpgradeLvl.L3:
                        myResource = VillageController.Instance.CannonLvl3;
                        break;
                }
                break;
            case BuildingType.Mortar:
                switch (_upgradeLevel)
                {
                    case UpgradeLvl.L1:
                        myResource = VillageController.Instance.MortarLvl1;
                        break;
                    case UpgradeLvl.L2:
                        myResource = VillageController.Instance.MortarLvl2;
                        break;
                    case UpgradeLvl.L3:
                        myResource = VillageController.Instance.MortarLvl3;
                        break;
                }
                break;
        }

        Text.text = "Food: " + myResource.FoodCost + "\n";
        Text.text += "Wood: " + myResource.WoodCost + "\n";
        Text.text += "Stones: " + myResource.StoneCost + "\n";
        Text.text += "Iron: " + myResource.IronCost;
    }
}
