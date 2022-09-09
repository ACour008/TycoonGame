using UnityEngine;

public interface IComponentBuilder<T>
{
    public T BuildComponent(GameGrid<T> grid, int xCoord, int yCoord, float xPosition, float yPosition, int layerOrder, Transform parent);
}
