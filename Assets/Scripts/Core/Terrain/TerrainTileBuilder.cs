using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTileBuilder : IComponentBuilder<TerrainTile>
{
    TerrainGenerator terrainGenerator;

    public TerrainTileBuilder(TerrainGenerator terrainGenerator)
    {
        this.terrainGenerator = terrainGenerator;

    }

    public TerrainTile BuildComponent(GameGrid<TerrainTile> grid, int xCoord, int yCoord, float xPosition, float yPosition, int layerOrder, Transform parent)
    {
        GameObject terrainObject = new GameObject("terrain", typeof(SpriteRenderer), typeof(TerrainTile), typeof(PolygonCollider2D));
        TerrainTile terrainTile = terrainObject.GetComponent<TerrainTile>();
        TerrainData tileData = terrainGenerator.GetTerrainTypeForCoordinates(xCoord, yCoord);
        PolygonCollider2D collider = terrainObject.GetComponent<PolygonCollider2D>();

        TerrainTileType type = tileData.TileType;
        float height = tileData.TileHeight / 100;
        
        terrainTile.Initialize(grid, type, terrainGenerator.TileSprites, xPosition, yPosition + height, layerOrder);

        terrainObject.transform.SetParent(parent);
        // terrainObject.transform.localScale = new Vector3(grid.CellSize.x, grid.CellSize.y, 1);

        collider.SetPath(0, new Vector2[4]
        {
            new Vector2(-0.5f, 0.025f),
            new Vector2(0f, -0.2f),
            new Vector2(0.5f, 0.025f),
            new Vector2(0f, 0.25f)
        });

        return terrainTile;
    }
}
