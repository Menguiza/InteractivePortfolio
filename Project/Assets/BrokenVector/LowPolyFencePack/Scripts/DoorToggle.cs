using System;
using UnityEngine;

namespace BrokenVector.LowPolyFencePack
{
    public class DoorToggle : MonoBehaviour
    {
        [SerializeField] private GameObject col; 

        [SerializeField] private DoorController doorController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                doorController.ToggleDoor();
                col.SetActive(false);
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