using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    Character character;
    public void End()
    {
       
    }

    public void Init()
    {
        SoundManager.__instance__.PlayMusic(MapUI.GetSound("RoadMusic"));
        character = MapUI.Get<Character>("Character");
    }

    public void Update()
    {
        character.MoveUpdate();
    }
}
