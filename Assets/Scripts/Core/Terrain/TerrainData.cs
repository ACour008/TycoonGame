using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "Terrain Data", menuName = "ScriptableObjects/Terrain")]
public class TerrainData : ScriptableObject
{
    [SerializeField] TerrainTileType tileType;
    [SerializeField] Sprite tileSprite;
    [SerializeField] float maxHeight;
    [SerializeField] float tileHeight;

    public TerrainTileType TileType { get => tileType; }
    public Sprite TileSprite { get => tileSprite; }
    public float MaxHeight { get => maxHeight; }
    public float TileHeight { get => tileHeight; }
}
