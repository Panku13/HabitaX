using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private Material previewMaterial;
    [SerializeField] private Material removeMaterial;
    [SerializeField] private Material invalidMaterial;

    private GameObject currentPreviewObject;
    private float currentRotation = 0f;
    private bool isValid = true;

    private Transform indicator; // 🔹 referencia al indicador

    public void SetIndicator(Transform indicatorTransform)
    {
        indicator = indicatorTransform;
    }

    public void StartShowingPlacementPreview(GameObject prefab, float startRotation = 0f)
    {
        StopShowingPreview();
        currentRotation = startRotation;

        currentPreviewObject = Instantiate(prefab);
        SetPreviewMaterial(currentPreviewObject, previewMaterial);
        UpdatePreviewRotation(currentRotation);
    }

    // 🔹 Ahora solo usamos el indicador en vez de crear un cubo
    public void StartShowingRemovePreview()
{
    StopShowingPreview();
    if (indicator != null)
    {
        // 🔹 Resetear a tamaño 1x1 cuando entramos a modo eliminar
        indicator.localScale = new Vector3(1f, 1f, 1f);

        var renderer = indicator.GetComponent<MeshRenderer>();
        if (renderer != null)
            renderer.material = removeMaterial;
    }
}


    public void StopShowingPreview()
    {
        if (currentPreviewObject != null)
            Destroy(currentPreviewObject);

        currentPreviewObject = null;

        // 🔹 restaurar indicador si existe
        if (indicator != null)
        {
            var renderer = indicator.GetComponent<MeshRenderer>();
            if (renderer != null)
                renderer.material = previewMaterial;
        }
    }

    public void UpdatePreviewPosition(Vector3 worldPosition)
    {
        if (currentPreviewObject != null)
            currentPreviewObject.transform.position = worldPosition;
    }

    public void UpdatePreviewRotation(float rotationY)
    {
        if (currentPreviewObject != null)
            currentPreviewObject.transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    // 🔹 Ahora mueve el indicador en lugar del cubo
    public void UpdateRemovePreview(Vector3 worldPosition, bool hasObject)
    {
        if (indicator == null) return;

        indicator.position = worldPosition;

        var renderer = indicator.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = hasObject ? removeMaterial : invalidMaterial;
        }
    }

    public void UpdateIndicator(Transform indicator, Vector3 worldPos, Vector2Int size)
    {
        if (indicator == null) return;

        indicator.position = worldPos;
        indicator.localScale = new Vector3(size.x, 1f, size.y);
    }

    public void SetPreviewValidity(bool valid)
    {
        if (currentPreviewObject == null) return;

        if (isValid != valid)
        {
            isValid = valid;
            Material mat = valid ? previewMaterial : invalidMaterial;
            SetPreviewMaterial(currentPreviewObject, mat);
        }
    }

    private void SetPreviewMaterial(GameObject obj, Material mat)
    {
        foreach (var renderer in obj.GetComponentsInChildren<MeshRenderer>())
        {
            var mats = renderer.materials;
            for (int i = 0; i < mats.Length; i++)
                mats[i] = mat;
            renderer.materials = mats;
        }
    }
}
