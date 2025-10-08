using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    private ObjectsDatabaseSO database;
    private Grid grid;
    private PreviewSystem previewSystem;
    private ObjectPlacer objectPlacer;
    private SoundFeedback soundFeedback;
    private GridData gridData;
    private Transform placementIndicator;

    private ObjectData objectData;
    private Vector3Int lastGridPosition;
    private int currentRotation;
    private bool isValidPosition;

    public PlacementState(int objectID, ObjectsDatabaseSO database, Grid grid, PreviewSystem previewSystem, ObjectPlacer objectPlacer, SoundFeedback soundFeedback, GridData gridData, Transform placementIndicator)
    {
        this.database = database;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.objectPlacer = objectPlacer;
        this.soundFeedback = soundFeedback;
        this.gridData = gridData;
        this.placementIndicator = placementIndicator;

        objectData = database.GetObjectData(objectID);

        if (objectData != null)
        {
            selectedObjectIndex = objectID;
            previewSystem.StartShowingPlacementPreview(objectData.Prefab, currentRotation);
            UpdateIndicatorSize(); // 🔹 ajusta el tamaño al iniciar
        }
        else
        {
            Debug.LogError($"❌ No se encontró el objeto con ID {objectID} en la base de datos.");
        }
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        if (objectData == null || gridData == null) return;

        if (!isValidPosition)
        {
           // soundFeedback?.PlaySound(SoundType.Invalid);
            return;
        }

        Vector3 worldPos = grid.CellToWorld(gridPosition);
        int index = objectPlacer.PlaceObject(objectData.Prefab, worldPos, Quaternion.Euler(0, currentRotation, 0));
        gridData.AddObjectAt(gridPosition, objectData.Size, index, objectData, Quaternion.Euler(0, currentRotation, 0));

        soundFeedback?.PlaySound(SoundType.Place);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        if (objectData == null || gridData == null) return;

        lastGridPosition = gridPosition;
        Vector3 worldPos = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePreviewPosition(worldPos);

        bool canPlace = gridData.CanPlaceObjectAt(gridPosition, objectData.Size);
        isValidPosition = canPlace;
        previewSystem.SetPreviewValidity(canPlace);

        // 🔹 actualizar la posición del indicador
        if (placementIndicator != null)
        {
            placementIndicator.position = worldPos;
        }
    }

    public void OnRotate()
    {
        if (objectData == null) return;

        currentRotation = (currentRotation + 90) % 360;
        previewSystem.UpdatePreviewRotation(currentRotation);

    }

    private void UpdateIndicatorSize()
    {
        if (placementIndicator != null && objectData != null)
        {
            placementIndicator.localScale = new Vector3(objectData.Size.x, 1, objectData.Size.y);
        }
    }
}
