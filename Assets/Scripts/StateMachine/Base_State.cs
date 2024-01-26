using System;
using UnityEngine;

public abstract class Base_State<EState> where EState : Enum
{
    public Base_State(EState key)
    {
        stateKey = key;
    }

    public EState stateKey;
    public abstract void InitializeState();
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();
    public abstract void OnTriggerEnter(Collider collider);
    public abstract void OnTriggerStay(Collider collider);
    public abstract void OnTriggerExit(Collider collider);

}
