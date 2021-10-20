using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingBattle : State
{
    AsyncOperation loadingScene;
    GameObject map;
    BattleStartAnimation battleStartAnimation;

    public void End()
    {

    }

    public void Init()
    {
        map = WorldUI.GetGameObject("Map");
        battleStartAnimation = WorldUI.Get<BattleStartAnimation>("BattleStartAnimator");
        SoundManager.__instance__.PlayMusic(WorldUI.GetSound("BattleMusic"));
        battleStartAnimation.StartAnimate((t) => { LoadBattleScene(); });
    }

    public void LoadBattleScene()
    {
        loadingScene = SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
    }
    public void Update()
    {
        if(loadingScene != null && loadingScene.isDone)
        {
            StateStack.Pop();
            StateStack.Push(new BattleState());
            map.SetActive(false);

            battleStartAnimation.EndAnimate();
        }
    }
}
