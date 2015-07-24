using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public GamePeriod Period;

    public delegate void GamePeriodDelegate();
    public event GamePeriodDelegate OnGamePeriodChange;

    //Collect
    public float CollectPeriodDuration = 0.0f;
    public float CollectDurationPerPeriod = 0.0f;

    private float _collectPeriodTimer = 0.0f;

    //Defense
    [HideInInspector]
    public int WaveCounter = 0;
    [HideInInspector]
    public int EnemiesCount = 0;
    public int BaseEnemiesCount = 0;
    public int EnemiesIncreasePerWaveValue = 0;

    private void Start()
    {
        EnemiesCount = BaseEnemiesCount;
    }
    
    private void Update()
    {
        if(VillageController.Instance.VillageHP == 0 )
        {
            Debug.LogWarning("Game Over");
            Debug.Break();
        }
        _collectPeriodTimer += Time.deltaTime;
        if(Period == GamePeriod.Collect)
        {
            if(_collectPeriodTimer > CollectPeriodDuration)
            {
                WaveCounter += 1;
                EnemiesCount = BaseEnemiesCount + EnemiesIncreasePerWaveValue * BaseEnemiesCount;
                OnGamePeriodChange();
            }
        }
        else
        {
            if(EnemiesCount == 0)
            {
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
