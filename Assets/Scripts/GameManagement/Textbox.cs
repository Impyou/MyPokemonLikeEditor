using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textbox : State
{
    private string text;
    private Action callback;
    public enum TargetTextbox { BATTLE_TEXTBOX , WORLD_TEXTBOX};
    private TargetTextbox targetTextbox;

    private GameObject textObject;
    private TextMesh textMesh;
    public Textbox(string text, TargetTextbox targetTextbox)
    {
        this.text = text;
        this.targetTextbox = targetTextbox;
    }

    public Textbox(string text, TargetTextbox targetTextbox, Action callback) : this(text, targetTextbox)
    {
        this.callback = callback;
    }

    public void End()
    {
        textObject.SetActive(false);
        if(callback != null)
            callback.Invoke();
    }

    public void Init()
    {
        switch (targetTextbox)
        {
            case TargetTextbox.BATTLE_TEXTBOX:
                textObject = BattleUI.GetGameObject("BattleTextbox");
                textMesh = textObject.GetComponentInChildren<TextMesh>();
                break;
            case TargetTextbox.WORLD_TEXTBOX:
                textObject = WorldUI.GetGameObject("WorldTextbox");
                textMesh = textObject.GetComponentInChildren<TextMesh>();
                break;
        }
        textObject.SetActive(true);
        textMesh.text = text;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StateStack.Pop();
        }
    }
}
