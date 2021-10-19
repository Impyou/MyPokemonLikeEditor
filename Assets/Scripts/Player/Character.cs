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

    void Start()
    {
        pos = Vector2Int.one;
    }

    // Update is called once per frame
    public void MoveUpdate()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(new Vector2Int(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(new Vector2Int(1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(new Vector2Int(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(new Vector2Int(0, -1));
        }else if(Input.GetKeyDown(KeyCode.P))
        {
            StateStack.Push(new Textbox("Your party is getting healed !!!", Textbox.TargetTextbox.WORLD_TEXTBOX, () => { HealParty(); }));
        }

        transform.position = GetPos();

    }

    private void HealParty()
    {
        party.HealParty();
    }

    Vector3 GetPos()
    {
        return new Vector3(pos.x * block_size + block_size/2f, pos.y * block_size - block_size / 2f, 0f);
    }

    private void Move(Vector2Int dir)
    {
        bool meetPokemon = false;

        if (tileMapValue.GetTile(pos.x + dir.x, pos.y + dir.y) == TilemapValue.TileType.WALL)
            return;
        if (tileMapValue.GetTile(pos.x + dir.x, pos.y + dir.y) == TilemapValue.TileType.GRASS &&
            Random.Range(0, 10) == 0)
        {
            meetPokemon = true;
        }

        pos += dir; 

        if (meetPokemon)
        {
            StateStack.Push(new StartingBattle());
        }
    }
}
