﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour 
{
    protected NavMeshAgent _myAgent;

    protected GameObject _target;
    protected bool _playerInSight = false;
    protected bool _playerReallyClose = false;

    protected float _hp;

    public bool IsDead { get; protected set; }

	// Use this for initialization
	void Start () 
    {
        _myAgent = GetComponent<NavMeshAgent>();
        if(_myAgent == null)
        {
            Debug.LogError("How the fuck is it possible?!");
        }
        _myAgent.SetDestination(Vector3.zero);
	}

    void OnEnable()
    {
        _hp = 50.0f;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if(_playerInSight)
        {
            if(_playerReallyClose)
            {
                Debug.Log("Attack this motherfucker!");
                Vector3 lookPos = _target.transform.position - transform.position;
                if(lookPos.magnitude >= 2.5f)
                {
                    _playerReallyClose = false;
                    _myAgent.Resume();
                }
            }
            else
            {
                _myAgent.SetDestination(_target.transform.position);
                if((_target.transform.position - transform.position).magnitude < 2.0f)
                {
                    _myAgent.Stop();
                    _playerReallyClose = true;
                }
            }
        }
	}

    public void DecreaseHealth(float dmg)
    {
        _hp -= dmg;
        if(_hp <= 0.0f)
        {
            _myAgent.Stop();
            _target = null;
            GetComponent<Collider>().enabled = false;
            _playerInSight = false;
            _playerReallyClose = false;
            IsDead = true;
            Destroy(gameObject, 1.0f);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = col.gameObject;
            _playerInSight = true;
            _playerReallyClose = false;
            //_myAgent.Stop();
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = null;
            _playerInSight = false;
            _playerReallyClose = false;
            _myAgent.SetDestination(Vector3.zero);
            //_myAgent.Resume();
        }
    }
}