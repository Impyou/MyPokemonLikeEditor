using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAnimation : State
{
    public List<Lock> locks = new List<Lock>();
    public class Lock
    {
        public bool isOpen;

        public void Open()
        {
            isOpen = true;
        }

        public bool IsClose()
        {
            return !isOpen;
        }
    }

    public Lock GetNewLock()
    {
        locks.Add(new Lock());
        return locks[locks.Count - 1];
    }

    public void End()
    {}

    public void Init()
    {}

    public void Update()
    {
        foreach(var _lock in locks)
        {
            if (_lock.IsClose())
                return;
        }
        StateStack.Pop();
    }
}
