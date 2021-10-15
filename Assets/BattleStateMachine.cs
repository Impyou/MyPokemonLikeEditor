using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    public static BattleStateMachine __instance__ = null;
    private IBattleState currentState;

    private bool newState = true;
    public void SetState(IBattleState newBattleState)
    {
        currentState = newBattleState;
        newState = true;
    }


    public void Start()
    {
        if (__instance__ != null)
            Debug.LogError("Multiple Battle State Machine !!!");

        __instance__ = this;
        SetState(new SelectAction());
    }

    public void Update()
    {
        if(newState)
        {
            currentState.Init();
            newState = false;
            return;
        }

        currentState.Update();
    }
}
