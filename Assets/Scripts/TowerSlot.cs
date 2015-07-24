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
    public GameObject Tower;
    public GameObject Cannon;
    public GameObject Mortar;

    public Card TowerCard;
    public Card CannonCard;
    public Card MortarCard;

    protected TowerKindEnum _towerKind = TowerKindEnum.None;

    private bool _isPlayerCloseEnough = false;

    void Update()
    {
        //if(_isPlayerCloseEnough)
        {
            if(Input.GetKey(KeyCode.B))
            {
                switch(_towerKind)
                {
                    case TowerKindEnum.None:
                        TowerCard.gameObject.SetActive(true);
                        TowerCard.SetTowerSlot(this);
                        CannonCard.gameObject.SetActive(true);
                        CannonCard.SetTowerSlot(this);
                        MortarCard.gameObject.SetActive(true);
                        MortarCard.SetTowerSlot(this);
                        break;
                    case TowerKindEnum.Tower:
                        CannonCard.gameObject.SetActive(true);
                        CannonCard.SetTowerSlot(this);
                        MortarCard.gameObject.SetActive(true);
                        MortarCard.SetTowerSlot(this);
                        break;
                    case TowerKindEnum.Cannon:
                        TowerCard.gameObject.SetActive(true);
                        TowerCard.SetTowerSlot(this);
                        MortarCard.gameObject.SetActive(true);
                        MortarCard.SetTowerSlot(this);
                        break;
                    case TowerKindEnum.Mortar:
                        TowerCard.gameObject.SetActive(true);
                        TowerCard.SetTowerSlot(this);
                        CannonCard.gameObject.SetActive(true);
                        CannonCard.SetTowerSlot(this);
                        break;
                }
            }
        }
    }

    public void SetUpgrade(TowerKindEnum newTower)
    {
        _towerKind = newTower; switch (_towerKind)
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
                break;
            case TowerKindEnum.Cannon:
                Tower.SetActive(false);
                Cannon.SetActive(true);
                Mortar.SetActive(false);
                break;
            case TowerKindEnum.Mortar:
                Tower.SetActive(false);
                Cannon.SetActive(false);
                Mortar.SetActive(true);
                break;
        }
        TowerCard.gameObject.SetActive(false);
        CannonCard.gameObject.SetActive(false);
        MortarCard.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == GameManager.Instance.PlayerLayer)
        {
            _isPlayerCloseEnough = true;
            Debug.Log("Hello");
            //Turn on A button image
        }
    }
}
