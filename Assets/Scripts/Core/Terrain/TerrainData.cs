using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "Terrain Data", menuName = "ScriptableObjects/Terrain")]
public class TerrainData : ScriptableObject
{
    [SerializeField] TerrainTileType tileType;
    [SerializeField] Sprite tileSprite;
    [SerializeField] float maxHeight;

    public TerrainTileType TileType { get => tileType; }
    public Sprite TileSprite { get => tileSprite; }
    public float MaxHeight { get => maxHeight; }
}
