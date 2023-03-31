using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages scene change.
/// It requires a Animator, and certain references defined on "Assignable parameters".
/// </summary>
[RequireComponent(typeof(Animator))]
public class SceneChange : MonoBehaviour
{
    //Assignable parameters
    [SerializeField] private int targetSceneIndex;
    [SerializeField] private AnimationClip easeClip;
    
    //Utility parameters
    private Animator anim;
    
    void Start()
    {
        //Animator reference (before class context the Animator component was required)
        anim = GetComponent<Animator>();
    }

    #region Methods

    /// <summary>
    /// Activates trigger on Animator ("anim" in this case) and invokes "ChangeSceneDelayed" with easeClip length as
    /// delay time. 
    /// </summary>
    public void ChangeScene()
    {
        anim.SetTrigger("EaseOut");
        Invoke("ChangeSceneDelayed", easeClip.length);
    }

    /// <summary>
    /// Changes scene with certain index using SceneManager
    /// </summary>
    void ChangeSceneDelayed()
    {
        SceneManager.LoadScene(targetSceneIndex);
    }

    #endregion
}
