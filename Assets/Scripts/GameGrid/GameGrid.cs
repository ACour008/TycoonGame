using System;
using UnityEngine;
using MiiskoWiiyaas.Utils.Arrays;
using MiiskoWiiyaas.Utils.GameObjects;

public class GameGrid<T>
{
    int height;
    int width;
    Vector2 cellSize;
    Vector2 originPosition;
    float heightOffset;
    T[] cells;

    public int Height { get => height; }
    public int Width { get => width; }
    public Vector2 CellSize { get => cellSize; }
    public Vector2 Origin { get => originPosition; }
    public T[] Cells {get => cells; }

    public GameGrid(int height, int width, Vector2 cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;
        this.originPosition = new Vector2();
        this.heightOffset = 0;

        this.cells = new T[this.height * this.width];


    }

    public GameGrid(int height, int width, Vector2 cellSize, Vector2 originPosition)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.heightOffset = 0;

        this.cells = new T[this.height * this.width];
    }

    // Turn Func into delegate with optional Transform
    public void CreateGrid(Func<GameGrid<T>, int, int, float, float, int, Transform, T> builderFunction)
    {
        GameObject parent = GameObjectUtils.CreateEmptyGameObject("TerrainGrid", new Vector3(0, 0, 0));
        int layerOffset = -1;

        for (int i = 0; i < width * height; i++)
        {
            if (i % height == 0) layerOffset++;

            float x = (i % width) + originPosition.x;
            float y = (i / width) + originPosition.y;

            Vector2 isoPosition = CalculateIsometricPosition(x, y);
            int layerOrder = (i % width) + layerOffset;

            cells[i] = builderFunction(this, i % width, i / width, isoPosition.x, isoPosition.y * -1, layerOrder, parent.transform);
        }
    }

    public Vector2 GetPositionAt(int x, int y)
    {
        Vector2 isoPosition = CalculateIsometricPosition(x, y) + originPosition;
        isoPosition.y *= -1;

        return isoPosition;
    }

    public T GetValueAt(int x, int y)
    {
        if (!CoordinatesAreWithinRange(x, y)) return default(T);

        return cells[ArrayUtils.Map2DTo1D(x, y, width)]; 
    }

    public void SetValueAt(int x, int y, T value)
    {
        if (!CoordinatesAreWithinRange(x, y)) return;

        cells[ArrayUtils.Map2DTo1D(x, y, width)] = value;
    }

    private bool CoordinatesAreWithinRange(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }

    // To Utils
    public Vector2 CalculateIsometricPosition(float x, float y)
    {
        return new Vector2( (x - y) * (cellSize.x / 2) / 100, (x + y) * (cellSize.y / 2) / 100);
    }

    // To Utils
    public Vector2Int CalculateCartesianPosition(float isoX, float isoY)
    {
        return new Vector2Int( (int)(2 * isoY + isoX), (int)(2 * isoY - isoX));
        // return new Vector2( (isoX + isoY) * (cellSize.x / 2) / 100, (isoX - isoY) * (cellSize.y / 2) / 100);
    }
}
