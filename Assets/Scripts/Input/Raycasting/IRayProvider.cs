using UnityEngine;

public interface IRayProvider
{
    public Ray GetRay(Vector2 position);
}
