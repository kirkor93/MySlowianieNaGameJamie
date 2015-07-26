using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Motivator : MonoBehaviour {

    public bool Move
    {
        get;
        set;
    }

    private bool _moveLeft = true;
    private AudioSource _myAudioSource;
    private Animator _myAnimator;
    private RectTransform _myRectTransform;
    private Vector3 _destinationPosition;
    private Vector3 _basePosition;
    private Text _mySpeach;
    private float _moveTimer;
    private bool _stupidFlag = true;

    private string _collectSpeach;
    private string _defenseSpeach;
    private string _justDoItSpeach = "Just do it!!!";

	// Use this for initialization
	void Start () {
        _myAudioSource = GetComponent<AudioSource>();
        _myAnimator = GetComponent<Animator>();
        _myRectTransform = GetComponent<RectTransform>();
        _mySpeach = GetComponentInChildren<Text>();
        _basePosition = _myRectTransform.localPosition;
        _destinationPosition = _basePosition + new Vector3(-400f, 0f, 0f);
        _basePosition += new Vector3(100.0f, 0.0f, 0.0f);
        GameManager.Instance.OnGamePeriodChange += OnGamePeriodChange;
        _collectSpeach = string.Format("You have to collect: {0} food, {1} wood, {2} stone, {3} iron", 
            VillageController.Instance.FoodNeededValue, VillageController.Instance.WoodNeededValue, VillageController.Instance.StoneNeededValue, VillageController.Instance.IronNeededValue);
        _defenseSpeach = string.Format("You have to defend village from {0} vikings", GameManager.Instance.EnemiesCount);
	}
	
    private void OnGamePeriodChange()
    {
        Move = true;
        _moveLeft = true;
        _moveTimer = 0.0f;
        _myAnimator.speed = 0.0f;
        Invoke("ChangeSpeach", 0.1f);
    }

    private void ChangeSpeach()
    {
        _collectSpeach = string.Format("You have to collect: {0} food, {1} wood, {2} stone, {3} iron",
            VillageController.Instance.FoodNeededValue, VillageController.Instance.WoodNeededValue, VillageController.Instance.StoneNeededValue, VillageController.Instance.IronNeededValue);
        _defenseSpeach = string.Format("You have to defend village from {0} vikings", GameManager.Instance.EnemiesCount);
        if(GameManager.Instance.Period == GamePeriod.Collect)
        {
            _mySpeach.text = _collectSpeach;
        }
        else
        {
            _mySpeach.text = _defenseSpeach;
        }
    }

    private void ChangeSpeachOnJustDoIt()
    {
        //_mySpeach.text = _justDoItSpeach;
        _myAudioSource.Play();
    }

    public void OnSpeachFinished()
    {
        _moveLeft = false;
        _moveTimer = 0.0f;
    }

	// Update is called once per frame
	void Update () {
	    if(Move)
        {
            _moveTimer += Time.deltaTime;
            if(_moveLeft)
            {
                _myRectTransform.localPosition = Vector3.Lerp(_basePosition, _destinationPosition, _moveTimer * 0.7f);
                if(_moveTimer * 0.7f > 1.0f && _stupidFlag)
                {
                    _myAnimator.speed = 1.0f;
                    _myAnimator.Play("JustDoItAnimation",-1,0.0f);
                    _stupidFlag = false;
                }
            }
            else
            {
                _stupidFlag = true;
                _myRectTransform.localPosition = Vector3.Lerp(_destinationPosition, _basePosition, _moveTimer * 0.7f);
            }
        }
	}
}
