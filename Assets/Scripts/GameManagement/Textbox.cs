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

    public enum TextboxType { WAIT_KEY, ONE_FRAME, TIME_DEPENDENT};
    TextboxType type;

    public Textbox(string text, TargetTextbox targetTextbox, TextboxType type = TextboxType.WAIT_KEY)
    {
        this.text = text;
        this.targetTextbox = targetTextbox;
        this.type = type;
    }

    public Textbox(string text, TargetTextbox targetTextbox, Action callback, TextboxType type = TextboxType.WAIT_KEY) : this(text, targetTextbox, type : type)
    {
        this.callback = callback;
    }

    public void End()
    {
        textMesh.text = "";
        if(targetTextbox == TargetTextbox.WORLD_TEXTBOX)
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
                textObject.SetActive(true);
                break;
        }
        textMesh.text = text;
    }

    public void Update()
    {
        switch(type)
        {
            case TextboxType.WAIT_KEY:
                if (Input.GetKeyDown(KeyCode.Return))
                    StateStack.Pop();
                break;
            case TextboxType.ONE_FRAME:
                StateStack.Pop();
                break;
        }
    }
}
