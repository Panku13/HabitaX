using UnityEngine;
using UnityEngine.EventSystems; // ⬅️ Necesario para detectar si el mouse está sobre la UI

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private ObjectsDatabaseSO database;
    [SerializeField] private PreviewSystem previewSystem;
    [SerializeField] private ObjectPlacer objectPlacer;
    [SerializeField] private SoundFeedback soundFeedback;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform placementIndicator; // ⬅️ Indicador asignado en el inspector
    [SerializeField] private UIObjectStats uiStats; // arrastras el script en el inspector

    private IBuildingState currentState;
    private GridData gridData;

    private void Awake()
    {
        if (gridData == null)
            gridData = new GridData();
    }

    private void Start()
    {
        inputManager.OnClicked += HandleClick;
        inputManager.OnExit += HandleExit;
        inputManager.OnRotate += HandleRotate;
    }

    private void Update()
    {
        if (currentState == null) return;

        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        // 🔹 Mover indicador en la grilla
        if (placementIndicator != null)
        {
            placementIndicator.position = grid.CellToWorld(gridPos);
        }

        currentState.UpdateState(gridPos);
    }

    public void StartPlacement(int objectID)
    {
        uiStats.ShowObjectStats(objectID);

          currentState?.EndState();
    currentState = new PlacementState(objectID, database, grid, previewSystem, objectPlacer, soundFeedback, gridData, placementIndicator);

    }

    public void StartRemoving()
    {
        currentState?.EndState();
        currentState = new RemovingState(grid, previewSystem, objectPlacer, gridData, soundFeedback);
    }

    private void HandleClick()
    {
        // ⬅️ No ejecutar acciones si el mouse está sobre la UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        currentState?.OnAction(gridPos);
    }

    private void HandleExit()
{
    uiStats.ClearUI();
    
    currentState?.EndState();
    currentState = null;

}


    private void HandleRotate()
    {
        currentState?.OnRotate();
    }
}
