using UnityEngine;

public class RemovingState : IBuildingState
{
    private Grid grid;
    private PreviewSystem previewSystem;
    private ObjectPlacer objectPlacer;
    private GridData gridData;
    private SoundFeedback soundFeedback;

    public RemovingState(Grid grid, PreviewSystem previewSystem, ObjectPlacer objectPlacer, GridData gridData, SoundFeedback soundFeedback)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.objectPlacer = objectPlacer;
        this.gridData = gridData;
        this.soundFeedback = soundFeedback;

        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridObjectData data = gridData.GetObjectData(gridPosition);
        if (data == null)
        {
            // soundFeedback?.PlaySound(SoundType.Invalid);
            return;
        }

        objectPlacer.RemoveObjectAt(data.PlacedObjectIndex);
        gridData.RemoveObjectAt(gridPosition);
        soundFeedback?.PlaySound(SoundType.Remove);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool hasObject = gridData.HasObjectAt(gridPosition);
        previewSystem.UpdateRemovePreview(grid.CellToWorld(gridPosition), hasObject);
    }

    public void OnRotate()
    {
        // En el modo remover no se usa rotación
    }
}
