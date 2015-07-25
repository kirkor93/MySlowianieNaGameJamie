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
        _towerKind = newTower; 
        switch (_towerKind)
        {
            case TowerKindEnum.None:
                Tower.SetActive(false);
                Cannon.SetActive(false);
                Mortar.SetActive(false);
                break;
            case TowerKindEnum.Tower:
                Tower.SetActive(true);
                Cannon.SetActive(false);
                Mortar.SetActive(false);
                TowerSlotMesh.enabled = false;
                break;
            case TowerKindEnum.Cannon:
                Tower.SetActive(false);
                Cannon.SetActive(true);
                Mortar.SetActive(false);
                TowerSlotMesh.enabled = false;
                break;
            case TowerKindEnum.Mortar:
                Tower.SetActive(false);
                Cannon.SetActive(false);
                Mortar.SetActive(true);
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
