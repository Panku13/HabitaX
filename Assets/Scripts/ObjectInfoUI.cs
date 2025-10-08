using UnityEngine;
using TMPro; // Usamos TextMeshPro

public class UIObjectStats : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private TextMeshProUGUI nameText;

    // Dimensiones
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI widthText;
    [SerializeField] private TextMeshProUGUI lengthText;
    [SerializeField] private TextMeshProUGUI areaText;

    // Recursos
    [SerializeField] private TextMeshProUGUI oxygenText;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI foodText;

    [Header("Referencias de Sistema")]
    [SerializeField] private ObjectsDatabaseSO database;

    private ObjectData currentObject;

    public void ShowObjectStats(int objectID)
    {
        currentObject = database.GetObjectData(objectID);

        if (currentObject == null)
        {
            ClearUI();
            return;
        }

        // 🔹 Asignamos valores
        nameText.text   = $"Nombre: {currentObject.Name}";

        // Dimensiones
        volumeText.text = $"Volumen: {currentObject.Volume} m³";
        heightText.text = $"Altura: {currentObject.Height} m";
        areaText.text   = $"Área: {currentObject.Area} m²";
        widthText.text  = $"Ancho: {currentObject.Width} m";
        lengthText.text = $"Largo: {currentObject.Length} m";


        // Recursos
        oxygenText.text = $"Oxígeno: {currentObject.Oxygen} kPa";
        energyText.text = $"Energía: {currentObject.Energy} kWh";
        waterText.text  = $"Agua: {currentObject.Water} kg";
        foodText.text   = $"Comida: {currentObject.Food} kg";
    }

    public void ClearUI()
    {
        nameText.text = "";

        volumeText.text = "";
        heightText.text = "";
        areaText.text = "";
        widthText.text = "";
        lengthText.text = "";


        oxygenText.text = "";
        energyText.text = "";
        waterText.text = "";
        foodText.text = "";
    }
}
