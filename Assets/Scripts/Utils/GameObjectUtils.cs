using System.Collections;
using UnityEngine;
using TMPro;

namespace MiiskoWiiyaas.Utils.GameObjects
{
    public static class GameObjectUtils
    {
        public static IEnumerator DestroyAfterSeconds(GameObject gameObject, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            GameObject.Destroy(gameObject);
        }

        public static GameObject CreateEmptyGameObject(string objectName, Vector3 position)
        {
            GameObject newGameObject = new GameObject();
            newGameObject.name = objectName;
            newGameObject.transform.position = position;

            return newGameObject;
        }
    }
}
