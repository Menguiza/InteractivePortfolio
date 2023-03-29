using System;
using UnityEngine;

namespace BrokenVector.LowPolyFencePack
{
    /// <summary>
    /// This class toggles the door animation.
    /// The gameobject of this script has to have the DoorController script which needs an Animator component
    /// and some kind of Collider which detects your mouse click applied.
    /// </summary>
    [RequireComponent(typeof(DoorController))]
	public class DoorToggle : MonoBehaviour
    {
        [SerializeField] private GameObject collider; 

        private DoorController doorController;

        void Awake()
        {
            doorController = GetComponent<DoorController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                doorController.ToggleDoor();
                collider.SetActive(false);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                doorController.ToggleDoor();
                collider.SetActive(true);
            }
        }
    }
}