using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 5;
    [SerializeField] float zoomScaleFactor = 1.1f;
    [SerializeField] MinMax<float> orthoSizeMinMax;

    Camera mainCamera;
    CameraPanner cameraPanner;
    CameraZoomer cameraZoomer;
    CameraDragger cameraDragger;

    public CameraPanner Panner { get => cameraPanner; }
    public CameraZoomer Zoomer { get => cameraZoomer; }
    public CameraDragger Dragger { get => cameraDragger; }

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        cameraPanner = new CameraPanner(mainCamera, panSpeed);
        cameraZoomer = new CameraZoomer(mainCamera, orthoSizeMinMax, zoomScaleFactor);
        cameraDragger = new CameraDragger(mainCamera);
    }

    public void SetBoundaries(MinMax<float> xMinMax, MinMax<float> yMinMax)
    {
        cameraPanner.SetBoundaries(xMinMax, yMinMax);
        cameraZoomer.SetBoundaries(xMinMax, yMinMax);
        cameraDragger.SetBoundaries(xMinMax, yMinMax);
    }


    public void Drag(Vector2 mousePosition) => cameraDragger.Drag(mousePosition);
    public void SetDragOrigin(Vector2 mousePosition) => cameraDragger.DragOrigin = mainCamera.ScreenToWorldPoint(mousePosition);
    public void SetDragVelocity(Vector2 velocity) => cameraDragger.DragVelocity = velocity;

}