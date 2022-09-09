using UnityEngine;


public class TerrainTile : MonoBehaviour, IClickable
{
    [SerializeField] bool liveChange;
    [SerializeField] TerrainTileType terrainType;
    GameGrid<TerrainTile> grid;
    SpriteRenderer spriteRenderer;
    Sprite[] tileSprites;

    public GameObject GameObject { get => this.gameObject; }

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.tag = "Terrain";
        gameObject.layer = 7;
    }

    public void OnPointerClicked()
    {
        Debug.Log("Clicked some terrain!");
    }

    public void Initialize(GameGrid<TerrainTile> grid, TerrainTileType type, Sprite[] tileSprites, float xPosition, float yPosition, int layerOrder)
    {
        this.grid = grid;
        this.tileSprites = tileSprites;

        SetType(type);
        SetPosition(xPosition, yPosition);
        SetLayerOrder(layerOrder);
        
    }

    private void SetLayerOrder(int order)
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = order;
    }

    private void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    private void SetType(TerrainTileType type)
    {
        terrainType = type;

        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tileSprites[(int)terrainType];
    }


    private void Update()
    {
        if (liveChange) SetType(terrainType);
    }
}