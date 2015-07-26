using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    //[HideInInspector]
    public GamePeriod Period;
    public List<SpawnPoint> SpawnPoints;
    public Transform RestrictionPointOne;
    public Transform RestrictionPointTwo;

    public delegate void GamePeriodDelegate();
    public event GamePeriodDelegate OnGamePeriodChange;

    //Collect
    public float CollectPeriodDuration = 5.0f;
    public float CollectDurationPerPeriod = 0.0f;

    private float _collectPeriodTimer = 0.0f;

    private bool _changeSoundFlag = false;
    private bool _muteSoundPhase = true;
    private float _soundMuteTimer = 0.0f;

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

    public AudioClip[] Clips;

    private AudioSource _myAudioSource;
    private float _baseVolume;

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
        _myAudioSource = GetComponent<AudioSource>();
        _baseVolume = _myAudioSource.volume;
        EnemiesCount = BaseEnemiesCount;
        _myAudioSource.Play();
        OnGamePeriodChange();
    }
    
    private void Update()
    {
//        Debug.Log("Period = " + Period);
        if(VillageController.Instance.VillageHP <= 0 )
        {
            Debug.LogWarning("Game Over");
            Debug.Break();
        }
        if(_changeSoundFlag)
        {
            _soundMuteTimer += Time.deltaTime;
            if (_muteSoundPhase)
            {
                _myAudioSource.volume -= Time.deltaTime * _baseVolume;
                if(_soundMuteTimer >= 0.5f)
                {
                    if(Period == GamePeriod.Collect)
                    {
                        _myAudioSource.clip = Clips[0];
                        _myAudioSource.Play();
                    }
                    else
                    {
                        _myAudioSource.clip = Clips[1];
                        _myAudioSource.Play();
                    }
                    _muteSoundPhase = false;
                }
            }
            else
            {
                _myAudioSource.volume += Time.deltaTime * _baseVolume;
                if (_soundMuteTimer >= 0.5f)
                {
                    _changeSoundFlag = false;
                }
            }
            
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
                _soundMuteTimer = 0.0f;
                _changeSoundFlag = true;
                _muteSoundPhase = true;
                _baseVolume += 0.2f;
            }
        }
        else
        {
            if(EnemiesCount == 0)
            {
                Period = GamePeriod.Collect;
                _collectPeriodTimer = 0.0f;
                OnGamePeriodChange();
                _soundMuteTimer = 0.0f;
                _changeSoundFlag = true;
                _muteSoundPhase = true;
                _baseVolume -= 0.2f;
            }
        }
    }

}

public enum GamePeriod
{
    Collect,
    Defense
}
