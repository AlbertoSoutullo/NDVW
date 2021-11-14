using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{

    [SerializeField][Range(1, 100)] private uint mapSize = 5;
    [SerializeField] private Transform wallPrefab = null;
    [SerializeField] private Terrain terrainPrefab = null;
    [SerializeField] private float cellSize = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        var map = MapGeneratorMaze.Generate(checked((int) this.mapSize));
        Draw(map);
    }

    private void Draw(WallState[,] map)
    {
        var terrain = Instantiate(this.terrainPrefab, transform);
        terrain.terrainData.size = new Vector3(this.mapSize * cellSize, 10.0f, this.mapSize * cellSize);

        for (uint z = this.mapSize; z > 0; --z)
        {
            for (uint x = 0; x < this.mapSize; ++x)
            {
                var cell = map[z - 1, x];
                var position = new Vector3(x * this.cellSize, 0.0f, z * this.cellSize);

                if (cell.HasFlag(WallState.UP))
                {
                    var wall = Instantiate(this.wallPrefab, transform) as Transform;
                    wall.position = position;
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var wall = Instantiate(this.wallPrefab, transform) as Transform;
                    wall.position = position;
                    wall.eulerAngles = new Vector3(0.0f, 90, 0.0f);
                }

                if (x == this.mapSize - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var wall = Instantiate(this.wallPrefab, transform) as Transform;
                        wall.position = position + new Vector3(this.cellSize, 0.0f, 0.0f);
                        wall.eulerAngles = new Vector3(0.0f, 90, 0.0f);
                    }
                }

                if (z == 1)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var wall = Instantiate(this.wallPrefab, transform) as Transform;
                        wall.position = position + new Vector3(0.0f, 0.0f, -this.cellSize);
                    } 
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}