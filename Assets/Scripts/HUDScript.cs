using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour 
{
    public Text FoodText;
    public Text WoodText;
    public Text IronText;
    public Text StoneText;
    public Text TimeLeftToAttackText;
    public Image GameOverImage;

	// Use this for initialization
	void Start () 
    {
        GameManager.Instance.OnGamePeriodChange += OnGamePeriodChange;
        VillageController.Instance.OnGameOver += OnGameOver;
	}

    private void OnGameOver(object sender, EventArgs eventArgs)
    {
        GameOverImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
	void Update () 
    {
        FoodText.text = VillageController.Instance.FoodValue.ToString();
        WoodText.text = VillageController.Instance.WoodValue.ToString();
        IronText.text = VillageController.Instance.IronValue.ToString();
        StoneText.text = VillageController.Instance.StoneValue.ToString();
        TimeLeftToAttackText.text = ConvertTimeToText();
	}

    string ConvertTimeToText()
    {
        if(GameManager.Instance.Period == GamePeriod.Defense)
        {
            //So if there is no time left then we want to display how many enemies left
            return GameManager.Instance.EnemiesCount.ToString();
        }
        float timeLeft = GameManager.Instance.TimeLeft;
        int minutes = (int)(timeLeft / 60.0f);
        int seconds = (int)((timeLeft - minutes * 60.0f));
        string time = "";
        if(minutes < 10)
        {
            time += "0";
        }
        time += minutes.ToString();
        time += ":";
        if(seconds < 10)
        {
            time += "0";
        }
        time += seconds.ToString();
        return time;
    }

    void OnGamePeriodChange()
    {
        //gameObject.SetActive(GameManager.Instance.Period == GamePeriod.Collect);
    }
}
