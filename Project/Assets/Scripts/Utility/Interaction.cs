using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CanvasGroup cnv;

    public UnityEvent Interacted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Alpha");
            cnv.alpha = 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Interacted.Invoke();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("NoAlpha");
            cnv.alpha = 0;
        }
    }
}
