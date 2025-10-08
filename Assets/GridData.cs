using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private Dictionary<Vector3Int, GridObjectData> placedObjects = new();

    public bool CanPlaceObjectAt(Vector3Int position, Vector2Int size)
    {
        foreach (var cell in GetOccupiedCells(position, size))
        {
            if (placedObjects.ContainsKey(cell))
                return false;
        }
        return true;
    }

    public void AddObjectAt(Vector3Int position, Vector2Int size, int index, ObjectData data, Quaternion rotation)
    {
        foreach (var cell in GetOccupiedCells(position, size))
        {
            placedObjects[cell] = new GridObjectData
            {
                PlacedObjectIndex = index,
                Size = size,
                Data = data,
                Rotation = rotation
            };
        }
    }

    public bool HasObjectAt(Vector3Int position)
    {
        return placedObjects.ContainsKey(position);
    }

    public GridObjectData GetObjectData(Vector3Int position)
    {
        if (placedObjects.TryGetValue(position, out var data))
            return data;
        return null;
    }

    public void RemoveObjectAt(Vector3Int position)
    {
        if (!placedObjects.TryGetValue(position, out var data))
            return;

        foreach (var cell in GetOccupiedCells(position, data.Size))
            placedObjects.Remove(cell);
    }

    private IEnumerable<Vector3Int> GetOccupiedCells(Vector3Int position, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                yield return new Vector3Int(position.x + x, position.y + y, position.z);
            }
        }
    }
}

public class GridObjectData
{
    public int PlacedObjectIndex;
    public Vector2Int Size;
    public ObjectData Data;
    public Quaternion Rotation;
}
