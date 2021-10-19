using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingBattle : State
{
    AsyncOperation loadingScene;
    GameObject map;

    public void End()
    {

    }

    public void Init()
    {
        loadingScene = SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
        map = WorldUI.GetGameObject("Map");
        SoundManager.__instance__.PlayMusic(WorldUI.GetSound("BattleMusic"));
    }

    public void Update()
    {
        if(loadingScene.isDone)
        {
            StateStack.Pop();
            StateStack.Push(new BattleState());
            map.SetActive(false);
        }
    }
}
