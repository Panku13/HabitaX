using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ObjectsDatabaseSO", menuName = "ScriptableObjects/ObjectsDatabase")]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectsData = new List<ObjectData>();

    public ObjectData GetObjectData(int ID)
    {
        foreach (var obj in objectsData)
        {
            if (obj.ID == ID)
                return obj;
        }
        Debug.LogWarning($"No se encontró el objeto con ID {ID}");
        return null;
    }
}

[System.Serializable]
public class ObjectData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject Prefab { get; private set; }

    [Header("Dimensiones (Geometría)")]
    [field: SerializeField] public float Volume { get; private set; }      // m³
    [field: SerializeField] public float Height { get; private set; }      // m
       [field: SerializeField] public float Area { get; private set; }        // m²
    [field: SerializeField] public float Width { get; private set; }       // m
    [field: SerializeField] public float Length { get; private set; }      // m
 

    [Header("Recursos")]
    [field: SerializeField] public float Oxygen { get; private set; }      // kPa
    [field: SerializeField] public float Energy { get; private set; }      // kWh
    [field: SerializeField] public float Water { get; private set; }       // kg
    [field: SerializeField] public float Food { get; private set; }        // kg
}
