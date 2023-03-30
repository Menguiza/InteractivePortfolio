using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneChange : MonoBehaviour
{
    [SerializeField] private int targetSceneIndex;
    [SerializeField] private AnimationClip easeClip;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeScene()
    {
        anim.SetTrigger("EaseOut");
        Invoke("ChangeSceneDelayed", easeClip.length);
    }

    void ChangeSceneDelayed()
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
}
