using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, Base_State<EState>> States = new();
    protected Base_State<EState> currentState;

    protected bool isTransitioningState = false;
    void Awake()
    {
        currentState.InitializeState();
    }
    void Start()
    {
        currentState.EnterState();
    }
    void Update()
    {
        EState nextStateKey = currentState.GetNextState();

        if(!isTransitioningState && nextStateKey.Equals(currentState.stateKey))
        {
            currentState.UpdateState();
        }
        else if(!isTransitioningState)
        {
            TransitionToState(nextStateKey);
        }
    }

    private void TransitionToState(EState nextStateKey)
    {
        isTransitioningState = true;
        currentState.ExitState();
        currentState = States[nextStateKey];
        currentState.EnterState();
        isTransitioningState = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(collider);
    }
    void OnTriggerStay(Collider collider)
    {
        currentState.OnTriggerStay(collider);
    }
    void OnTriggerExit(Collider collider)
    {
        currentState.OnTriggerExit(collider);
    }
}
