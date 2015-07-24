using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    //[HideInInspector]
    public GamePeriod Period;

    public delegate void GamePeriodDelegate();
    public event GamePeriodDelegate OnGamePeriodChange;

    //Collect
    public float CollectPeriodDuration = 5.0f;
    public float CollectDurationPerPeriod = 0.0f;

    private float _collectPeriodTimer = 0.0f;

    //Defense
    [HideInInspector]
    public int WaveCounter = 0;
    [HideInInspector]
    public int EnemiesCount = 0;
    public int BaseEnemiesCount = 1;
    public int EnemiesIncreasePerWaveValue = 1;

    private void Start()
    {

        EnemiesCount = BaseEnemiesCount;
    }
    
    private void Update()
    {
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
                WaveCounter += 1;
                EnemiesCount = BaseEnemiesCount + EnemiesIncreasePerWaveValue * BaseEnemiesCount;
                //Debug shieet
                if (WaveCounter == 1) EnemiesCount = 1;
                OnGamePeriodChange();
            }
        }
        else
        {
            if(EnemiesCount == 0)
            {
                Period = GamePeriod.Collect;
                _collectPeriodTimer = 0.0f;
                //Debug shieeet
                CollectPeriodDuration = float.MaxValue;
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
