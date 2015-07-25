using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager> {

    const string XAxis = "Horizontal";
    const string YAxis = "Vertical";
    const string Action = "ActionButton";
    const string Tower = "TowerButton";
    const string Cannon = "CannonButton";
    const string Mortar = "MortarButton";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    string BuildPlayerButton(string buttonName, PlayerIndexEnum playerIndex)
    {
        string toReturn = buttonName;
        switch(playerIndex)
        {
            case PlayerIndexEnum.PlayerOne:
                toReturn += "PlayerOne";
                break;
            case PlayerIndexEnum.PlayerTwo:
                toReturn += "PlayerTwo";
                break;
            case PlayerIndexEnum.PlayerThree:
                toReturn += "PlayerThree";
                break;
            case PlayerIndexEnum.PlayerFour:
                toReturn += "PlayerFour";
                break;
            default:
                toReturn += "PlayerOne";
                break;
        }
        return toReturn;
    }

    public Vector3 GetLeftStick(PlayerIndexEnum playerIndex)
    {
        return new Vector3(Input.GetAxis(BuildPlayerButton(XAxis, playerIndex)), 0.0f, Input.GetAxis(BuildPlayerButton(YAxis, playerIndex)));
    }

    public bool GetAButton(PlayerIndexEnum playerIndex)
    {
        return Input.GetButtonDown(BuildPlayerButton(Action, playerIndex));
    }

    public bool GetXButton(PlayerIndexEnum playerIndex)
    {
        return Input.GetButtonDown(BuildPlayerButton(Mortar, playerIndex));
    }

    public bool GetYButton(PlayerIndexEnum playerIndex)
    {
        return Input.GetButtonDown(BuildPlayerButton(Cannon, playerIndex));
    }

    public bool GetBButton(PlayerIndexEnum playerIndex)
    {
        return Input.GetButtonDown(BuildPlayerButton(Tower, playerIndex));
    }
}
