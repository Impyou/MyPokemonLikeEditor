using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStack : MonoBehaviour
{
    private static StateStack __instance__ = null;

    private Stack<State> stackState;
    public StateStack()
    {
        if (__instance__ != null)
            Debug.LogError("Multiple Statestack !!!");

        __instance__ = this;

        stackState = new Stack<State>();
    }

    public static void Pop()
    {
        __instance__._Pop();
    }
    public static void Push(State state)
    {
        __instance__._Push(state);
    }

    public void Start()
    {
        StateStack.Push(new SelectAction());
    }
    public void _Pop()
    {
        var state = stackState.Pop();
        state.End();
    }

    public void _Push(State state)
    {
        stackState.Push(state);
        state.Init();
    }

    public void Update()
    {
        stackState.Peek().Update();
    }
}
