using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraInitializer : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Vector3 transparencySortAxis;


    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

        GraphicsSettings.transparencySortMode = TransparencySortMode.CustomAxis;
        GraphicsSettings.transparencySortAxis = transparencySortAxis;

        //mainCamera.transparencySortMode = TransparencySortMode.CustomAxis;
        //mainCamera.transparencySortAxis = transparencySortAxis;

    }
}
