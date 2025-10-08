using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private List<GameObject> placedObjects = new();

    public int PlaceObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject newObject = Instantiate(prefab, position, rotation);
        placedObjects.Add(newObject);
        return placedObjects.Count - 1;
    }

    public void RemoveObjectAt(int index)
    {
        if (index >= 0 && index < placedObjects.Count && placedObjects[index] != null)
        {
            Destroy(placedObjects[index]);
            placedObjects[index] = null;
        }
    }
}
