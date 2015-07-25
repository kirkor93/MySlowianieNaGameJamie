using UnityEngine;
using System.Collections;

public enum TowerKindEnum
{
    None,
    Tower,
    Cannon,
    Mortar
}

public class TowerSlot : MonoBehaviour
{
    protected PlayerController _nearestPlayerController;

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

    private bool _isPlayerCloseEnough = false;

    void Update()
    {
        if(_isPlayerCloseEnough)
        {
            if (InputManager.Instance.GetAButton(_nearestPlayerController.PlayerIndex))
            {
                AButton.SetActive(false);
                switch(_towerKind)
                {
                    case TowerKindEnum.None:
                        TowerCard.gameObject.SetActive(true);
                        TowerCard.SetTowerSlot(this);
                        TowerCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        CannonCard.gameObject.SetActive(true);
                        CannonCard.SetTowerSlot(this);
                        CannonCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        MortarCard.gameObject.SetActive(true);
                        MortarCard.SetTowerSlot(this);
                        MortarCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        break;
                    case TowerKindEnum.Tower:
                        TowerCard.gameObject.SetActive(true);
                        TowerCard.SetTowerSlot(this);
                        TowerCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        CannonCard.gameObject.SetActive(true);
                        CannonCard.SetTowerSlot(this);
                        CannonCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        MortarCard.gameObject.SetActive(true);
                        MortarCard.SetTowerSlot(this);
                        MortarCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        break;
                    case TowerKindEnum.Cannon:
                        TowerCard.gameObject.SetActive(true);
                        TowerCard.SetTowerSlot(this);
                        TowerCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        CannonCard.gameObject.SetActive(true);
                        CannonCard.SetTowerSlot(this);
                        CannonCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        MortarCard.gameObject.SetActive(true);
                        MortarCard.SetTowerSlot(this);
                        MortarCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        break;
                    case TowerKindEnum.Mortar:
                        TowerCard.gameObject.SetActive(true);
                        TowerCard.SetTowerSlot(this);
                        TowerCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        CannonCard.gameObject.SetActive(true);
                        CannonCard.SetTowerSlot(this);
                        CannonCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        CannonCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        MortarCard.gameObject.SetActive(true);
                        MortarCard.SetTowerSlot(this);
                        MortarCard.SetPlayerIndex(_nearestPlayerController.PlayerIndex);
                        break;
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
                break;
            case TowerKindEnum.Tower:
                switch(prevTowerKind)
                {
                    case TowerKindEnum.Tower:
                        if(Tower.activeSelf)
                        {
                            Tower.SetActive(false);
                            TowerUpgrade2.SetActive(true);
                        }
                        else if(TowerUpgrade2.activeSelf)
                        {
                            TowerUpgrade2.SetActive(false);
                            TowerUpgrade3.SetActive(true);
                        }
                        break;
                    default:
                        Tower.SetActive(true);
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
                        }
                        else if(CannonUpgrade2.activeSelf)
                        {
                            CannonUpgrade2.SetActive(false);
                            CannonUpgrade3.SetActive(true);
                        }
                        break;
                    default:
                        Cannon.SetActive(true);
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
                        }
                        else if (MortarUpgrade2.activeSelf)
                        {
                            MortarUpgrade2.SetActive(false);
                            MortarUpgrade3.SetActive(true);
                        }
                        break;
                    default:
                        Mortar.SetActive(true);
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
            _nearestPlayerController = col.gameObject.GetComponent<PlayerController>();
            _isPlayerCloseEnough = true;
            AButton.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _isPlayerCloseEnough = false;
            AButton.SetActive(false);
            TowerCard.gameObject.SetActive(false);
            CannonCard.gameObject.SetActive(false);
            MortarCard.gameObject.SetActive(false);
        }
    }
}
