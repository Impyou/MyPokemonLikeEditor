using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public TilemapValue tileMapValue;
    public int x_pos, y_pos;
    public float block_size = 0.5f;
    void Start()
    {
        x_pos = 1;
        y_pos = 1;
        Debug.Log(y_pos);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(0, -1);
        }

        transform.position = GetPos();

    }

    Vector3 GetPos()
    {
        return new Vector3(x_pos * block_size + block_size/2f, y_pos * block_size - block_size / 2f, 0f);
    }

    private void Move(int x_dir, int y_dir)
    {
        bool meetPokemon = false;

        if (tileMapValue.GetTile(x_pos + x_dir, y_pos + y_dir) == TilemapValue.TileType.WALL)
            return;
        if (tileMapValue.GetTile(x_pos + x_dir, y_pos + y_dir) == TilemapValue.TileType.GRASS)
            meetPokemon = true;

        x_pos += x_dir;
        y_pos += y_dir;

        if(meetPokemon)
            SceneManager.LoadScene("BattleScene");
    }
}
