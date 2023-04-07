using UnityEngine;

public class EnvironmentChanges : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private Color ambientColor, lightColor;
    
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = false;
        RenderSettings.skybox = mat;
        RenderSettings.ambientSkyColor = ambientColor;
        RenderSettings.ambientLight = lightColor;
    }
}
