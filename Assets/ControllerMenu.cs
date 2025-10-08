using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject opcionesPanel; // Panel de opciones que se activa/desactiva
    public GameObject habitad;

    // --- BOTONES PRINCIPALES ---
    public void JugarMarte()
    {
        SceneManager.LoadScene("Mars");
    }
    public void JugarLuna()
    {
        SceneManager.LoadScene("Moon"); 
    }

    public void ActivarEleccion()
    {
        habitad.SetActive(true);
    }

    public void DesactivarEleccion()
    {
        habitad.SetActive(false);
    }

    public void Opciones()
    {
        if (opcionesPanel != null)
            opcionesPanel.SetActive(true); // Activa el panel de opciones
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Credits"); // Cambia a la escena de créditos
    }

    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit(); // Cierra el juego (no funciona en el editor)
    }

    // --- FUNCIONES EXTRA ---
    public void CerrarOpciones()
    {
        if (opcionesPanel != null)
            opcionesPanel.SetActive(false); // Desactiva el panel
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Regresa al menú principal
    }
}
