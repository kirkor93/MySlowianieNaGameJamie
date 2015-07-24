using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour 
{
    public string ButtonName;
    public TowerKindEnum TowerKind;

    protected TowerSlot _towerSlot;

    void Update()
    {
        if(Input.GetButtonDown(ButtonName))
        {
            _towerSlot.SetUpgrade(TowerKind);
        }
    }

    public void SetTowerSlot(TowerSlot ts)
    {
        _towerSlot = ts;
    }
}
