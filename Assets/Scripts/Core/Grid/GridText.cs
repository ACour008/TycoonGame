using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridText: IComponentBuilder<int>
{

    private int index = 0;

    public int BuildComponent(GameGrid<int> gameGrid, int xCoord, int yCoord, float xPosition, float yPosition, int layerOrder, Transform parent = null)
    {
        int position = index++;

        GameObject canvasGameObject = new GameObject();
        Canvas canvas = canvasGameObject.AddComponent<Canvas>();
        RectTransform canvasRT = canvas.GetComponent<RectTransform>();

        canvasGameObject.transform.SetParent(parent);
        canvasGameObject.name = "Text Canvas";

        canvas.worldCamera = Camera.main;

        canvasRT.position = new Vector3(xPosition, yPosition, 0);
        canvasRT.sizeDelta = gameGrid.CellSize;
        canvasRT.pivot = new Vector2(1, 0);


        GameObject textGameObject = new GameObject();
        TextMeshProUGUI guiText = textGameObject.AddComponent<TextMeshProUGUI>();
        RectTransform guiTextRT = guiText.GetComponent<RectTransform>();

        textGameObject.name = "Text_" + position;
        textGameObject.transform.SetParent(canvasGameObject.transform);

        guiTextRT.anchoredPosition3D = new Vector3(0,0,0);
        guiTextRT.sizeDelta = canvasRT.sizeDelta;

        guiText.text = position.ToString();
        guiText.fontSize = 5;

        return position;
    }
}
