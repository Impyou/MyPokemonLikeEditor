using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapValue : MonoBehaviour
{
    public enum TileType {NONE, WALL};
    public int width, height;
    [SerializeField] public TileType[,] tileTypes; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
