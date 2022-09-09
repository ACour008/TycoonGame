using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragger
{
    private Camera camera;
    private Vector2 dragOrigin;
    private Vector3 dragVelocity;
    private MinMax<float> xPositionLimits;
    private MinMax<float> yPositionLimits;


    public Vector2 DragOrigin { get => dragOrigin; set => dragOrigin = value; }
    public Vector3 DragVelocity { get => dragVelocity; set => dragVelocity = value; }

    public CameraDragger(Camera camera)
    {
        this.camera = camera;
    }

    public void SetBoundaries(MinMax<float> xMinMax, MinMax<float> yMinMax)
    {
        xPositionLimits = xMinMax;
        yPositionLimits = yMinMax;
    }

    public void Drag(Vector2 mousePosition)
    {
        mousePosition = camera.ScreenToWorldPoint(mousePosition);

        Vector2 deltaPosition = dragOrigin - mousePosition;
        Vector3.SmoothDamp(deltaPosition, mousePosition, ref dragVelocity, 0.1f, 250f, Time.deltaTime);

        camera.transform.position += new Vector3(deltaPosition.x, deltaPosition.y, 0) * dragVelocity.magnitude * Time.deltaTime;

        float clampedX = Mathf.Clamp(camera.transform.position.x, xPositionLimits.Min, xPositionLimits.Max);
        float clampedY = Mathf.Clamp(camera.transform.position.y, yPositionLimits.Min, yPositionLimits.Max);
        camera.transform.position = new Vector3(clampedX, clampedY, camera.transform.position.z);

    }
}
