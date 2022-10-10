using UnityEngine;
using TMPro;


public class GameGridComponent : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private Vector2 cellSize;
    [SerializeField] private Vector2 originPosition;
    [SerializeField] private float isoHeightOffset;
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private CameraBoundarySetter cameraBoundarySetter;

    TerrainTileBuilder terrainBuilder;
    GameGrid<TerrainTile> gameGrid;

    public GameGrid<TerrainTile> GameGrid { get => gameGrid; }

    void Start()
    {
        terrainGenerator.Setup(width, height);
        terrainBuilder = new TerrainTileBuilder(terrainGenerator);

        gameGrid = new GameGrid<TerrainTile>(height, width, cellSize, originPosition, isoHeightOffset);
        gameGrid.CreateGrid(terrainBuilder.BuildComponent);

        cameraBoundarySetter.SetupBoundaries(gameGrid, isoHeightOffset);
    }
}
