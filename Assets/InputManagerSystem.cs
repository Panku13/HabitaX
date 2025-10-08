using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public event Action OnClicked;
    public event Action OnRotate;
    public event Action OnExit; // 🔹 Nuevo evento para salir

    private Camera mainCamera;
    [SerializeField] private LayerMask placementLayer; // 🔹 Solo raycast contra la grilla

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.R))
            OnRotate?.Invoke();

        // 🔹 Escape cancela la colocación
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    public Vector3 GetSelectedMapPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, placementLayer))
            return hitInfo.point;
        return Vector3.zero;
    }
}
