using UnityEngine;

/// <summary>
/// This class manages environment changes.
/// It apply specific parameters to the render settings.
/// </summary>
public class EnvironmentChanges : MonoBehaviour
{
    //Assignable parameters
    [SerializeField] private Material mat;
    [SerializeField] private Color ambientColor, lightColor;
    
    void Start()
    {
        //Method call to trigger main behavior of this class
        ApplySettings();
    }

    /// <summary>
    /// Use unity built-in reference to render settings and sets specific parameters
    /// to fog, skybox, ambientSkyColor and ambientLight.
    /// </summary>
    void ApplySettings()
    {
        RenderSettings.fog = false;
        RenderSettings.skybox = mat;
        RenderSettings.ambientSkyColor = ambientColor;
        RenderSettings.ambientLight = lightColor;
    }
}
