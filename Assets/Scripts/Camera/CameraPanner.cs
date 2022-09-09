using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner
{
    Camera camera;
    float speed;
    MinMax<float> xPositionLimits;
    MinMax<float> yPositionLimits;

    public CameraPanner(Camera camera, float speed)
    {
        this.camera = camera;
        this.speed = speed;
    }

    public void Pan(Vector2 direction)
    {
        // Change to allow for value clamping;
        //if ((camera.transform.position.x >= xPositionLimits.Min || camera.transform.position.x <= xPositionLimits.Max) &&
        //    (camera.transform.position.y >= yPositionLimits.Min || camera.transform.position.y <= yPositionLimits.Max))
        //{
        //    camera.transform.Translate(direction * speed * Time.deltaTime);
        //}

        Vector2 dir = direction * speed * Time.deltaTime;

        camera.transform.position = new Vector3(
            Mathf.Clamp(camera.transform.position.x + dir.x, xPositionLimits.Min, xPositionLimits.Max),
            Mathf.Clamp(camera.transform.position.y + dir.y, yPositionLimits.Min, yPositionLimits.Max),
            camera.transform.position.z
            );
    }

    public void SetBoundaries(MinMax<float> xMinMax, MinMax<float> yMinMax)
    {
        xPositionLimits = xMinMax;
        yPositionLimits = yMinMax;
    }
}
