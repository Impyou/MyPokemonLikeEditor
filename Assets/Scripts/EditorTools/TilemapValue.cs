using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapValue : MonoBehaviour
{
    public enum TileType {NONE, WALL, GRASS};

    [HideInInspector] public int width, height;
    [HideInInspector] public TileType[] tileTypes;

    public TileType GetTile(int x, int y)
    {
        return tileTypes[y * width + x];
    }

    public void SetTile(int x, int y, TileType value)
    {
        tileTypes[y * width + x] = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
