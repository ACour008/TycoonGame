/***********************************************************************
 * 
 * Algorithm adapted from Sebastian Lague: https://youtu.be/WP-Bm65Q-1Y
 *
 **********************************************************************/

using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] int seed;
    [SerializeField] int octives = 4;           // Number of generated noise maps that are added together
    [SerializeField] float lacunarity = 2f;     // Controls increase in frequency of octives
    [SerializeField] float persistence = 0.5f;  // Controls the decrease in amplitude of each octive.
    [SerializeField] float scale = 1f;
    [SerializeField] TerrainData[] terrainRegions;

    int width; int height;
    float[] noiseMap;
    Sprite[] tileSprites;
    float[] tileHeights;
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
            float x = (i % width) / scale;
            float y = (i / width) / scale;

            float amplitude = 1;
            float frequency = 1;
            float noiseHeight = 0;

            float halfWidth = width / 2;
            float halfHeight = height / 2;

            for (int oct = 0; oct < octives; oct++)
            {
                float sampleX = (x - halfWidth) * frequency + octiveOffsets[oct].x;
                float sampleY = (y - halfHeight) * frequency + octiveOffsets[oct].y;

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
            float offsetX = rdm.Next(-100000, 100000);
            float offsetY = rdm.Next(-100000, 100000);
            octiveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        return octiveOffsets;
    }

    public TerrainData GetTerrainTypeForCoordinates(int x, int y)
    {
        float noiseHeight = noiseMap[x * width + y];

        for (int i = 0; i < terrainRegions.Length; i++)
        {
            TerrainData data = terrainRegions[i];
            if (noiseHeight <= data.MaxHeight)
            {
                return data;
            }
        }
        // Probably should raise an error...?
        return terrainRegions[0];

    }

    private Sprite[] GetTileSpritesFromTerrainData()
    {
        Sprite[] sprites = new Sprite[terrainRegions.Length];

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = terrainRegions[i].TileSprite;
        }

        return sprites;
    }
}
