using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Vector2Int mapSize;
    public Vector2 scale = new Vector2(1,1);
    public int octaves = 1;

    public Grid grid;

    public GameObject hex;

    public static byte[,] map;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {
        map = new byte[mapSize.x, mapSize.y];

        for(int y = 0; y < mapSize.y; y++)
        {
            for(int x = 0; x < mapSize.x; x++)
            {
                map[x, y] = (byte)x;
                Instantiate(hex, grid.GetCellCenterLocal(new Vector3Int(x, y, map[x, y])), Quaternion.identity, transform);
            }
        }
    }
}
