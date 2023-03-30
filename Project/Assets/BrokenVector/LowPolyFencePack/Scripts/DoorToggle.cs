using UnityEngine;
using UnityEngine.Events;

namespace BrokenVector.LowPolyFencePack
{
    public class DoorToggle : MonoBehaviour
    {
        [SerializeField] private GameObject col;
        [SerializeField] private DoorController doorController;

        public UnityEvent Entering;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                doorController.ToggleDoor();
                col.SetActive(false);
                Entering.Invoke();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                doorController.ToggleDoor();
                col.SetActive(true);
            }
        }
    }
}