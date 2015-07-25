using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    //[HideInInspector]
    public GamePeriod Period;
    public List<SpawnPoint> SpawnPoints;

    public delegate void GamePeriodDelegate();
    public event GamePeriodDelegate OnGamePeriodChange;

    //Collect
    public float CollectPeriodDuration = 5.0f;
    public float CollectDurationPerPeriod = 0.0f;

    private float _collectPeriodTimer = 0.0f;

    public float TimeLeft
    {
        get
        {
            return CollectPeriodDuration - _collectPeriodTimer;
        }
    }

    //Defense
    [HideInInspector]
    public int WaveCounter = 0;
    [HideInInspector]
    public int EnemiesCount = 0;
    public int BaseEnemiesCount = 1;
    public int EnemiesIncreasePerWaveValue = 1;

    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public GameObject PlayerThree;
    public GameObject PlayerFour;

    void OnEnable()
    {
        string[] connectedControllers = Input.GetJoystickNames();
        switch(connectedControllers.Length)
        {
            case 4:
            default:
                EnablePlayers(true, true, true, true);
                break;
            case 3:
                EnablePlayers(true, true, true, false);
                break;
            case 2:
                EnablePlayers(true, true, false, false);
                break;
            case 1:
                EnablePlayers(true, false, false, false);
                break;
            case 0:
                EnablePlayers(false, false, false, false);
                break;
        }
    }

    void EnablePlayers(bool playerOne, bool playerTwo, bool playerThree, bool playerFour)
    {
        PlayerOne.SetActive(playerOne);
        PlayerTwo.SetActive(playerTwo);
        PlayerThree.SetActive(playerThree);
        PlayerFour.SetActive(playerFour);
    }

    private void Start()
    {
        EnemiesCount = BaseEnemiesCount;
        OnGamePeriodChange();
    }
    
    private void Update()
    {
        Debug.Log("Period = " + Period);
        if(VillageController.Instance.VillageHP <= 0 )
        {
            Debug.LogWarning("Game Over");
            Debug.Break();
        }
        if(Period == GamePeriod.Collect)
        {
            _collectPeriodTimer += Time.deltaTime;
            if(_collectPeriodTimer > CollectPeriodDuration)
            {
                Period = GamePeriod.Defense;
                EnemiesCount = 0;
                foreach(SpawnPoint sp in SpawnPoints)
                {
                    sp.SpawnEnemies();
                    EnemiesCount += sp.Enemies.Count;
                }
                WaveCounter += 1;
                OnGamePeriodChange();
            }
        }
        else
        {
            if(EnemiesCount == 0)
            {
                Period = GamePeriod.Collect;
                _collectPeriodTimer = 0.0f;
                OnGamePeriodChange();
            }
        }
    }

}

public enum GamePeriod
{
    Collect,
    Defense
}
