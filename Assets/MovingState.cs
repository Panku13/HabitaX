using UnityEngine;

public class MovingState : IBuildingState
{
private Grid grid;
private PreviewSystem previewSystem;
private ObjectPlacer objectPlacer;
private GridData gridData;
private SoundFeedback soundFeedback;
private Transform placementIndicator;

private GridObjectData selectedObject;
private int selectedIndex = -1;

public MovingState(Grid grid, PreviewSystem previewSystem, ObjectPlacer objectPlacer, GridData gridData, SoundFeedback soundFeedback, Transform indicator)
{
    this.grid = grid;
    this.previewSystem = previewSystem;
    this.objectPlacer = objectPlacer;
    this.gridData = gridData;
    this.soundFeedback = soundFeedback;
    this.placementIndicator = indicator;
}

public void EndState()
{
    previewSystem.StopShowingPreview();
    selectedObject = null;
    selectedIndex = -1;
}

public void OnAction(Vector3Int gridPosition)
{
    if (selectedObject == null)
    {
        var data = gridData.GetObjectData(gridPosition);
        if (data != null)
        {
            selectedObject = data;
            selectedIndex = data.PlacedObjectIndex;
            //soundFeedback?.PlaySound(SoundType.Select);
        }
    }
    else
    {
        bool valid = gridData.CanPlaceObjectAt(gridPosition, selectedObject.Size);
        previewSystem.SetPreviewValidity(valid);

        if (!valid)
        {
            //soundFeedback?.PlaySound(SoundType.Error);
            return;
        }

        Vector3 worldPos = grid.CellToWorld(gridPosition);
        objectPlacer.RemoveObjectAt(selectedIndex);
        selectedIndex = objectPlacer.PlaceObject(selectedObject.Data.Prefab, worldPos, selectedObject.Rotation);
        gridData.RemoveObjectAt(gridPosition);
        gridData.AddObjectAt(gridPosition, selectedObject.Size, selectedIndex, selectedObject.Data, selectedObject.Rotation);

        soundFeedback?.PlaySound(SoundType.Place);
        selectedObject = null;
        selectedIndex = -1;
    }
}

public void UpdateState(Vector3Int gridPosition)
{
    Vector3 worldPos = grid.CellToWorld(gridPosition);
    if (selectedObject != null)
    {
        previewSystem.UpdateIndicator(placementIndicator, worldPos, selectedObject.Size);
        bool valid = gridData.CanPlaceObjectAt(gridPosition, selectedObject.Size);
        previewSystem.SetPreviewValidity(valid);
    }
    else
    {
        placementIndicator.position = worldPos;
        placementIndicator.localScale = Vector3.one;
    }
}

public void OnRotate()
{
    // Rotación opcional para objetos en movimiento
}

}
