using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer
{
    Camera camera;
    float scaleFactor;
    MinMax<float> orthoSizeLimits;
    MinMax<float> xPositionLimits;
    MinMax<float> yPositionLimits;

    public CameraZoomer(Camera camera, MinMax<float> orthoSizeMinMax, float scaleFactor)
    {
        this.camera = camera;
        this.orthoSizeLimits = orthoSizeMinMax;
        this.scaleFactor = scaleFactor;
    }

    public void SetBoundaries(MinMax<float> xMinMax, MinMax<float> yMinMax)
    {
        xPositionLimits = xMinMax;
        yPositionLimits = yMinMax;
    }

    public void Zoom(float direction, Vector2 mousePosition = new Vector2())
    {
        if (scaleFactor < 0) return;
        if (scaleFactor < 1) scaleFactor += 1f;

        bool mousePositionIsZero = mousePosition.x == 0 && mousePosition.y == 0;

        if (!mousePositionIsZero) mousePosition = camera.ScreenToWorldPoint(mousePosition);

        Vector2 cameraPosition = new Vector2(camera.transform.position.x, camera.transform.position.y);
        Vector2 lengthBetweenMouseAndCamera = mousePosition - cameraPosition;

        Vector2 scaledLengthBasedOnOrthoSize = (direction > 0) ? lengthBetweenMouseAndCamera / scaleFactor : lengthBetweenMouseAndCamera * scaleFactor;
        Vector2 deltaLength = lengthBetweenMouseAndCamera - scaledLengthBasedOnOrthoSize;

        float targetOrthographicSize = (direction > 0) ? camera.orthographicSize / scaleFactor : camera.orthographicSize * scaleFactor;

        if (!mousePositionIsZero) SetCameraPosition(cameraPosition.x + deltaLength.x, cameraPosition.y + deltaLength.y);
        SetZoomLevel(Mathf.Clamp(targetOrthographicSize, orthoSizeLimits.Min, orthoSizeLimits.Max));

    }

    private void SetCameraPosition(float x, float y)
    {
        bool cameraOrthoSizeLargerThanItsLimits = camera.orthographicSize >= orthoSizeLimits.Max ||
            camera.orthographicSize <= orthoSizeLimits.Min;

        if (cameraOrthoSizeLargerThanItsLimits) return;

        camera.transform.position = new Vector3(
            Mathf.Clamp(x, xPositionLimits.Min, xPositionLimits.Max),
            Mathf.Clamp(y, yPositionLimits.Min, yPositionLimits.Max),
            camera.transform.position.z);
    }

    private void SetZoomLevel(float orthoSize)
    {
        camera.orthographicSize = orthoSize;
    }
}
