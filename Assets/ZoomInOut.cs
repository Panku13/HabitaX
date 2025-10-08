using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Orbit Settings")]
    public float rotationSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minDistance = 3f;
    public float maxDistance = 15f;

    [Header("Smoothing")]
    public float smoothTime = 0.1f; // menor = más responsivo, mayor = más lento
    private Vector2 rotationVelocity; 

    [Header("Limits")]
    public bool restrictBelow = true;  // true = no dejar bajar debajo del objeto
    public float minY = 0f;           // límite inferior (horizontal)
    public float maxY = 80f;          // límite superior

    private float distance;
    private float currentX;
    private float currentY;

    private float targetX;
    private float targetY;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("⚠ No se ha asignado un objeto al Target de la cámara.");
            return;
        }

        // Distancia inicial
        distance = Vector3.Distance(transform.position, target.position);

        // Valores iniciales (igual que en el inspector que me mostraste)
        transform.position = new Vector3(0, 10, -10);
        transform.rotation = Quaternion.Euler(45, 0, 0);

        // Guardamos la rotación inicial
        currentX = 0f;
        currentY = 45f;
        targetX = currentX;
        targetY = currentY;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Input de rotación con clic derecho
        if (Input.GetMouseButton(1))
        {
            targetX += Input.GetAxis("Mouse X") * rotationSpeed;
            targetY -= Input.GetAxis("Mouse Y") * rotationSpeed;

            if (restrictBelow)
                targetY = Mathf.Clamp(targetY, minY, maxY);
            else
                targetY = Mathf.Clamp(targetY, -80f, 80f);
        }

        // Suavizado (efecto de aceleración/desaceleración)
        currentX = Mathf.SmoothDamp(currentX, targetX, ref rotationVelocity.x, smoothTime);
        currentY = Mathf.SmoothDamp(currentY, targetY, ref rotationVelocity.y, smoothTime);

        // Zoom con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculamos la rotación
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 dir = new Vector3(0, 0, -distance);
        Vector3 position = rotation * dir + target.position;

        // Aplicamos a la cámara
        transform.position = position;
        transform.LookAt(target);
    }
}
