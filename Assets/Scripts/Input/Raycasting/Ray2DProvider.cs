using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray2DProvider : IRayProvider
{
    public Ray GetRay(Vector2 position) => Camera.main.ScreenPointToRay(position);
}
