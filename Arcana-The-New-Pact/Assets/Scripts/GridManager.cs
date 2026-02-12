using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]private int width, height;
    [SerializeField]private Tile wallTile,floorTile;
    [SerializeField]private Transform camera;

    private Dictionary<Vector2, Tile> tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var randomNumber = Random.Range(0, 10);
                var randomTile = new Tile();
                if (randomNumber > 8)
                {
                    randomTile = wallTile;
                }
                else
                {
                    randomTile = floorTile;
                }
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        camera.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);

    }



    public Tile GetTile(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }

}
