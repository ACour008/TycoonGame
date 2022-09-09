/***********************************************************************
 * 
 * Algorithm adapted from Sebastian Lague: https://youtu.be/WP-Bm65Q-1Y
 *
 **********************************************************************/

using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] int seed;
    [SerializeField] int octives = 4;
    [SerializeField] float lacunarity = 2f;
    [SerializeField] float persistence = 0.5f;
    [SerializeField] float scale = 1f;
    [SerializeField] TerrainData[] terrainRegions;

    int width; int height;
    float[] noiseMap;
    Sprite[] tileSprites;
    System.Random rdm;

    public Sprite[] TileSprites { get => tileSprites; }

    public void Setup(int gridWidth, int gridHeight)
    {
        width = gridWidth;
        height = gridHeight;
        rdm = new System.Random(seed);

        tileSprites = GetTileSpritesFromTerrainData();
        noiseMap = GenerateNoiseMap(width, height);
    }

    private float[] GenerateNoiseMap(int width, int height)
    {
        float[] noiseMap = new float[width * height];
        Vector2[] octiveOffsets = CreateOctiveOffsets();

        float maxHeight = float.MinValue;
        float minHeight = float.MaxValue;

        for (int i = 0; i < width * height; i++)
        {
            float x = i % width;
            float y = i / width;

            float amplitude = 1;
            float frequency = 1;
            float noiseHeight = 0;

            for (int oct = 0; oct < octives; oct++)
            {
                float sampleX = octiveOffsets[oct].x + x / width * scale * frequency;
                float sampleY = octiveOffsets[oct].y + y / height * scale * frequency;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                noiseHeight = perlinValue * amplitude;

                amplitude *= persistence;
                frequency *= lacunarity;
            }

            if (noiseHeight > maxHeight)
            {
                maxHeight = noiseHeight;
            }
            else if (noiseHeight < minHeight)
            {
                minHeight = noiseHeight;
            }

            noiseMap[i] = noiseHeight;
        }

        return Normalize(noiseMap, minHeight, maxHeight);
    }

    private float[] Normalize(float[] noiseMap, float minHeight, float maxHeight)
    {
        for (int i  = 0; i < noiseMap.Length; i++)
        {
            noiseMap[i] = Mathf.InverseLerp(minHeight, maxHeight, noiseMap[i]);
        }

        return noiseMap;
    }

    private Vector2[] CreateOctiveOffsets()
    {
        Vector2[] octiveOffsets = new Vector2[octives];

        for (int i = 0; i < octives; i++)
        {
            float offsetX = rdm.Next(-1000, 1000);
            float offsetY = rdm.Next(-1000, 1000);
            octiveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        return octiveOffsets;
    }

    public TerrainTileType GetTerrainTypeForCoordinates(int x, int y)
    {
        float noiseHeight = noiseMap[x * width + y];

        for (int i = 0; i < terrainRegions.Length; i++)
        {
            TerrainData data = terrainRegions[i];
            if (noiseHeight <= data.MaxHeight)
            {
                return data.TileType;
            }
        }

        return TerrainTileType.WATER;

    }

    private Sprite[] GetTileSpritesFromTerrainData()
    {
        Sprite[] sprites = new Sprite[terrainRegions.Length];

        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = terrainRegions[i].TileSprite;
        }

        return sprites;
    }

}
