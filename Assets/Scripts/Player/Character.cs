using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public TilemapValue tileMapValue;
    public Vector2Int pos;
    public float block_size = 0.5f;

    public PokemonParty party;
    private bool isMoving = false;
    private float movingTimer;
    private PlayerAnimationDir currentAnimation;
    private SpriteRenderer spriteRenderer;

    [Serializable]
    public struct PlayerAnimationDir
    {
        public string name;
        public Sprite[] sprites;
    }

    public float moveAnimationTime = 0.15f;
    public List<PlayerAnimationDir> playerAnimationDirs;

    void Start()
    {
        pos = Vector2Int.one;
        spriteRenderer = GetComponent<SpriteRenderer>();
        party.InitNewParty();
    }

    // Update is called once per frame
    public void MoveUpdate()
    {
        if (isMoving)
        {
            UpdateFrame();
            return;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Move(new Vector2Int(-1, 0));
            currentAnimation = playerAnimationDirs[1];
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(new Vector2Int(1, 0));
            currentAnimation = playerAnimationDirs[3];
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(new Vector2Int(0, 1));
            currentAnimation = playerAnimationDirs[2];
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(new Vector2Int(0, -1));
            currentAnimation = playerAnimationDirs[0];
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            StateStack.Push(new Textbox("Your party is getting healed !!!", Textbox.TargetTextbox.WORLD_TEXTBOX, () => { HealParty(); }));
        }

    }

    private void HealParty()
    {
        party.HealParty();
    }

    void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    Vector2 GetPos(Vector2Int pos)
    {
        return new Vector2(pos.x * block_size + block_size/2f, pos.y * block_size - block_size / 2f);
    }

    private void Move(Vector2Int dir)
    {
        bool meetPokemon = false;

        if (tileMapValue.GetTile(pos.x + dir.x, pos.y + dir.y) == TilemapValue.TileType.WALL)
            return;
        if (tileMapValue.GetTile(pos.x + dir.x, pos.y + dir.y) == TilemapValue.TileType.GRASS &&
            UnityEngine.Random.Range(0, 10) == 0)
        {
            meetPokemon = true;
        }

        isMoving = true;
        movingTimer = 0f;
        Action<ITween<Vector2>> movementCallback = (t) => { SetPosition(t.CurrentValue); };
        Vector2 startPos = GetPos(pos);
        pos += dir;
        Vector2 endPos = GetPos(pos);

        Action<ITween<Vector2>> onComplete;

        if (meetPokemon)
        {
            onComplete = (t) => { 
                isMoving = false;
                StateStack.Push(new StartingBattle());
                SetFirstSprite();
            };
        }
        else
        {
            onComplete = (t) => { 
                isMoving = false; 
                SetFirstSprite(); 
            };
        }
        var tween = gameObject.Tween("Player Movement", startPos, endPos, moveAnimationTime, TweenScaleFunctions.Linear, movementCallback, onComplete);
    }

    private void SetFirstSprite()
    {
        spriteRenderer.sprite = currentAnimation.sprites[0];
    }

    private void UpdateFrame()
    {
        movingTimer += Time.deltaTime;
        int frameId = Mathf.Min((int)(movingTimer / moveAnimationTime * currentAnimation.sprites.Length), currentAnimation.sprites.Length - 1);
        spriteRenderer.sprite = currentAnimation.sprites[frameId];
    }
}
