using UnityEngine;

namespace UI
{
    public enum FadeType
    {
        EaseIn,
        EaseOut
    }
    
    /// <summary>
    /// This class manages scene change.
    /// It requires a Animator, and certain references defined on "Assignable parameters".
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class FadePanel : MonoBehaviour
    {
        [Header("Parameters")] 
        [SerializeField] private FadeType fadeType;
        
        //Utility parameters
        private Animator anim;
    
        void Start()
        {
            //Animator reference (before class context the Animator component was required)
            anim = GetComponent<Animator>();
        }

        private void TriggerFade()
        {
            anim.SetTrigger(fadeType.ToString());
        }
    }
}
