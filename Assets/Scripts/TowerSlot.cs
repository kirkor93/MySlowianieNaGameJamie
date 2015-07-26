using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TowerKindEnum
{
    None,
    Tower,
    Cannon,
    Mortar
}

public class TowerSlot : MonoBehaviour
{
    protected List<PlayerController> _nearestPlayerController = new List<PlayerController>();

    public GameObject AButton;

    public GameObject Tower;
    public GameObject Cannon;
    public GameObject Mortar;
    public GameObject TowerUpgrade2;
    public GameObject CannonUpgrade2;
    public GameObject MortarUpgrade2;
    public GameObject TowerUpgrade3;
    public GameObject CannonUpgrade3;
    public GameObject MortarUpgrade3;

    public Card TowerCard;
    public Card CannonCard;
    public Card MortarCard;

    public MeshRenderer TowerSlotMesh;

    protected TowerKindEnum _towerKind = TowerKindEnum.None;
    protected UpgradeLvl _upgradeLvl = UpgradeLvl.L1;

    private bool _isPlayerCloseEnough = false;

    void Update()
    {
        if(_isPlayerCloseEnough)
        {
            foreach(PlayerController pc in _nearestPlayerController)
            {
                if (InputManager.Instance.GetAButton(pc.PlayerIndex))
                {
                    AButton.SetActive(false);
                    switch (_towerKind)
                    {
                        case TowerKindEnum.None:
                            TowerCard.gameObject.SetActive(true);
                            TowerCard.SetTowerSlot(this);
                            TowerCard.SetPlayerIndex(pc.PlayerIndex);
                            TowerCard.SetUpgradeLevel(_upgradeLvl);
                            CannonCard.gameObject.SetActive(true);
                            CannonCard.SetTowerSlot(this);
                            CannonCard.SetPlayerIndex(pc.PlayerIndex);
                            CannonCard.SetUpgradeLevel(_upgradeLvl);
                            MortarCard.gameObject.SetActive(true);
                            MortarCard.SetTowerSlot(this);
                            MortarCard.SetPlayerIndex(pc.PlayerIndex);
                            MortarCard.SetUpgradeLevel(_upgradeLvl);
                            break;
                        case TowerKindEnum.Tower:
                            TowerCard.gameObject.SetActive(true);
                            TowerCard.SetTowerSlot(this);
                            TowerCard.SetPlayerIndex(pc.PlayerIndex);
                            TowerCard.SetUpgradeLevel(_upgradeLvl);
                            CannonCard.gameObject.SetActive(true);
                            CannonCard.SetTowerSlot(this);
                            CannonCard.SetPlayerIndex(pc.PlayerIndex);
                            CannonCard.SetUpgradeLevel(_upgradeLvl);
                            MortarCard.gameObject.SetActive(true);
                            MortarCard.SetTowerSlot(this);
                            MortarCard.SetPlayerIndex(pc.PlayerIndex);
                            MortarCard.SetUpgradeLevel(_upgradeLvl);
                            break;
                        case TowerKindEnum.Cannon:
                            TowerCard.gameObject.SetActive(true);
                            TowerCard.SetTowerSlot(this);
                            TowerCard.SetPlayerIndex(pc.PlayerIndex);
                            TowerCard.SetUpgradeLevel(_upgradeLvl);
                            CannonCard.gameObject.SetActive(true);
                            CannonCard.SetTowerSlot(this);
                            CannonCard.SetPlayerIndex(pc.PlayerIndex);
                            CannonCard.SetUpgradeLevel(_upgradeLvl);
                            MortarCard.gameObject.SetActive(true);
                            MortarCard.SetTowerSlot(this);
                            MortarCard.SetPlayerIndex(pc.PlayerIndex);
                            MortarCard.SetUpgradeLevel(_upgradeLvl);
                            break;
                        case TowerKindEnum.Mortar:
                            TowerCard.gameObject.SetActive(true);
                            TowerCard.SetTowerSlot(this);
                            TowerCard.SetPlayerIndex(pc.PlayerIndex);
                            TowerCard.SetUpgradeLevel(_upgradeLvl);
                            CannonCard.gameObject.SetActive(true);
                            CannonCard.SetTowerSlot(this);
                            CannonCard.SetPlayerIndex(pc.PlayerIndex);
                            CannonCard.SetPlayerIndex(pc.PlayerIndex);
                            CannonCard.SetUpgradeLevel(_upgradeLvl);
                            MortarCard.gameObject.SetActive(true);
                            MortarCard.SetTowerSlot(this);
                            MortarCard.SetPlayerIndex(pc.PlayerIndex);
                            MortarCard.SetUpgradeLevel(_upgradeLvl);
                            break;
                    }
                }
            }
        }
    }

    public void SetUpgrade(TowerKindEnum newTower)
    {
        TowerKindEnum prevTowerKind = _towerKind;
        _towerKind = newTower; 
        switch (_towerKind)
        {
            case TowerKindEnum.None:
                Tower.SetActive(false);
                Cannon.SetActive(false);
                Mortar.SetActive(false);
                _upgradeLvl = UpgradeLvl.L1;
                break;
            case TowerKindEnum.Tower:
                switch(prevTowerKind)
                {
                    case TowerKindEnum.Tower:
                        if(Tower.activeSelf)
                        {
                            Tower.SetActive(false);
                            TowerUpgrade2.SetActive(true);
                            _upgradeLvl = UpgradeLvl.L3;
                        }
                        else if(TowerUpgrade2.activeSelf)
                        {
                            TowerUpgrade2.SetActive(false);
                            TowerUpgrade3.SetActive(true);
                        }
                        break;
                    default:
                        Tower.SetActive(true);
                        _upgradeLvl = UpgradeLvl.L2;
                        break;
                }

                Cannon.SetActive(false);
                CannonUpgrade2.SetActive(false);
                CannonUpgrade3.SetActive(false);
                Mortar.SetActive(false);
                MortarUpgrade2.SetActive(false);
                MortarUpgrade3.SetActive(false);
                TowerSlotMesh.enabled = false;
                break;
            case TowerKindEnum.Cannon:
                switch(prevTowerKind)
                {
                    case TowerKindEnum.Cannon:
                        if(Cannon.activeSelf)
                        {
                            Cannon.SetActive(false);
                            CannonUpgrade2.SetActive(true);
                            _upgradeLvl = UpgradeLvl.L3;
                        }
                        else if(CannonUpgrade2.activeSelf)
                        {
                            CannonUpgrade2.SetActive(false);
                            CannonUpgrade3.SetActive(true);
                        }
                        break;
                    default:
                        Cannon.SetActive(true);
                        _upgradeLvl = UpgradeLvl.L2;
                        break;
                }

                Tower.SetActive(false);
                TowerUpgrade2.SetActive(false);
                TowerUpgrade3.SetActive(false);
                Mortar.SetActive(false);
                MortarUpgrade2.SetActive(false);
                MortarUpgrade3.SetActive(false);
                TowerSlotMesh.enabled = false;
                break;
            case TowerKindEnum.Mortar:
                switch (prevTowerKind)
                {
                    case TowerKindEnum.Mortar:
                        if (Mortar.activeSelf)
                        {
                            Mortar.SetActive(false);
                            MortarUpgrade2.SetActive(true);
                            _upgradeLvl = UpgradeLvl.L3;
                        }
                        else if (MortarUpgrade2.activeSelf)
                        {
                            MortarUpgrade2.SetActive(false);
                            MortarUpgrade3.SetActive(true);
                        }
                        break;
                    default:
                        Mortar.SetActive(true);
                        _upgradeLvl = UpgradeLvl.L2;
                        break;
                }

                Tower.SetActive(false);
                TowerUpgrade2.SetActive(false);
                TowerUpgrade3.SetActive(false);
                Cannon.SetActive(false);
                CannonUpgrade2.SetActive(false);
                CannonUpgrade3.SetActive(false);
                TowerSlotMesh.enabled = false;
                break;
        }
        TowerCard.gameObject.SetActive(false);
        CannonCard.gameObject.SetActive(false);
        MortarCard.gameObject.SetActive(false);
        AButton.SetActive(true);
        _isPlayerCloseEnough = true;
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AButton.SetActive(true);
            _nearestPlayerController.Add(col.gameObject.GetComponent<PlayerController>());
            _isPlayerCloseEnough = true;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();
            TowerCard.RemovePlayerIndex(pc.PlayerIndex);
            CannonCard.RemovePlayerIndex(pc.PlayerIndex);
            MortarCard.RemovePlayerIndex(pc.PlayerIndex);
            _nearestPlayerController.Remove(pc);
            _isPlayerCloseEnough = _nearestPlayerController.Count != 0;
            if(!_isPlayerCloseEnough)
            {
                AButton.SetActive(false);
                TowerCard.gameObject.SetActive(false);
                CannonCard.gameObject.SetActive(false);
                MortarCard.gameObject.SetActive(false);
            }
        }
    }
}
