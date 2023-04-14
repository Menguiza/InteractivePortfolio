using UnityEngine;

/// <summary>
/// This class manages a callable method.
/// </summary>
public class OpenURL : MonoBehaviour
{
    //Assignable parameters
    [SerializeField] private string url;

    /// <summary>
    /// Uses Unity built-in Application.OpenURL method to open an specific URL in Desktop's default browser.
    /// URL to be opened is defined by the variable "url". It might be assigned on the Unity Editor or it'll stay as default (" ").
    /// </summary>
    public void OpenHttp()
    {
        Application.OpenURL(url);
    }
}
