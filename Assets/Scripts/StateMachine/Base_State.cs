using System;
using UnityEngine;

public abstract class Base_State<EnumStates> where EnumStates : Enum
{
    public Base_State(EnumStates key)
    {
        stateKey = key;
    }

    public EnumStates stateKey;
    public abstract void InitializeState();
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EnumStates GetNextState();
    public abstract void OnTriggerEnter(Collider collider);
    public abstract void OnTriggerStay(Collider collider);
    public abstract void OnTriggerExit(Collider collider);

}
