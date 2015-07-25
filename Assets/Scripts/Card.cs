using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour 
{
    public TowerKindEnum TowerKind;

    protected TowerSlot _towerSlot;
    protected PlayerIndexEnum _playerIndex;

    void Update()
    {
        bool condition = false;
        switch(TowerKind)
        {
            case TowerKindEnum.Tower:
                condition = InputManager.Instance.GetBButton(_playerIndex);
                break;
            case TowerKindEnum.Cannon:
                condition = InputManager.Instance.GetYButton(_playerIndex);
                break;
            case TowerKindEnum.Mortar:
                condition = InputManager.Instance.GetXButton(_playerIndex);
                break;
        }
        if(condition)
        {
            _towerSlot.SetUpgrade(TowerKind);
        }
    }

    public void SetTowerSlot(TowerSlot ts)
    {
        _towerSlot = ts;
    }

    public void SetPlayerIndex(PlayerIndexEnum playerIndex)
    {
        _playerIndex = playerIndex;
    }
}
