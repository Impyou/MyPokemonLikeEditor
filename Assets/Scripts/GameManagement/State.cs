using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    public void Init();
    public void Update();
    public void End();
}
