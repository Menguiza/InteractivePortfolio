using System;
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

        private void Awake()
        {
            //Animator reference (before class context the Animator component was required)
            anim = GetComponent<Animator>();
        }

        void Start()
        {
            TriggerFade(fadeType);
        }

        public void TriggerFade(FadeType fadeType)
        {
            ResetAllTriggers();

            anim.SetTrigger(fadeType.ToString());
        }

        private void ResetAllTriggers()
        {
            foreach (var name in Enum.GetNames(typeof(FadeType)))
            {
                anim.ResetTrigger(name);
            }
        }
    }
}
