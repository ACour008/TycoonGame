using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundarySetter : MonoBehaviour
{
    [SerializeField] private Vector2 cameraBoundaryOffset;
    private CameraController cameraController;


    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    public void SetupBoundaries(GameGrid<TerrainTile> gameGrid, float isoHeightOffset)
    {
        MinMax<float> xMinMax = new MinMax<float>(gameGrid.GetPositionAt(0, gameGrid.Height).x, gameGrid.GetPositionAt(gameGrid.Width, 0).x);
        MinMax<float> yMinMax = new MinMax<float>(gameGrid.GetPositionAt(gameGrid.Width, gameGrid.Height).y, gameGrid.GetPositionAt(0, 0).y);

        xMinMax.Min += gameGrid.CellSize.x / 2 + cameraBoundaryOffset.x;
        xMinMax.Max -= gameGrid.CellSize.x / 2 + cameraBoundaryOffset.x;

        yMinMax.Min += gameGrid.CellSize.y / 2 + (isoHeightOffset * gameGrid.Width) + cameraBoundaryOffset.y;
        yMinMax.Max -= gameGrid.CellSize.y / 2;

        cameraController.SetBoundaries(xMinMax, yMinMax);
    }


}
