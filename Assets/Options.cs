using UnityEngine;
using UnityEngine.UI;
using TMPro; // Para Dropdown moderno (TextMeshPro)

public class OptionsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Slider fpsSlider;
    public Toggle vSyncToggle;
    public Toggle fullscreenToggle;
    public Slider brightnessSlider; // Extra
    public Slider mouseSensitivitySlider; // Extra
    public TMP_Dropdown languageDropdown; // Extra

    private Resolution[] resolutions;

    void Start()
    {
        // === Detectar resoluciones disponibles ===
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // === Inicializar volumen ===
        volumeSlider.value = AudioListener.volume;

        // === Inicializar FPS ===
        fpsSlider.value = Application.targetFrameRate > 0 ? Application.targetFrameRate : 60;

        // === Inicializar VSync ===
        vSyncToggle.isOn = QualitySettings.vSyncCount > 0;

        // === Inicializar pantalla completa ===
        fullscreenToggle.isOn = Screen.fullScreen;

        // === Calidad gráfica ===
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        // === Opciones extra iniciales ===
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f);
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        languageDropdown.value = PlayerPrefs.GetInt("Language", 0);
    }

    // === Cambiar resolución ===
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen, res.refreshRate);
    }

    // === Cambiar volumen ===
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    // === Cambiar FPS target ===
    public void SetFPS(float fps)
    {
        Application.targetFrameRate = (int)fps;
    }

    // === Activar/Desactivar VSync ===
    public void SetVSync(bool isOn)
    {
        QualitySettings.vSyncCount = isOn ? 1 : 0;
    }

    // === Activar/Desactivar pantalla completa ===
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // === Cambiar calidad gráfica ===
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // === Extra: Brillo ===
    public void SetBrightness(float brightness)
    {
        PlayerPrefs.SetFloat("Brightness", brightness);
        // Aplica un post-procesado o modifica material global aquí
    }

    // === Extra: Sensibilidad del mouse ===
    public void SetMouseSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        // Pásalo a tu script de cámara/jugador
    }

    // === Extra: Idioma ===
    public void SetLanguage(int languageIndex)
    {
        PlayerPrefs.SetInt("Language", languageIndex);
        // Aquí puedes conectar con tu sistema de localización
    }
}
