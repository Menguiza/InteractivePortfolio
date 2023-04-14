using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class manages interaction zones behavior.
/// </summary>
public class Interaction : MonoBehaviour
{
    //Events
    public UnityEvent Interacted, Enter, Leave;

    //Utility parameters
    private bool canInteract;
    
    private void Update()
    {
        //Method call to check constantly for an interaction
        CheckForInteraction();
    }

    /// <summary>
    /// Built-in method called by Unity when an object with a collider attached enters on a trigger collider attached to this.GameObject.
    /// It filters if the collider detected is tagged as player and triggers "Enter" event. 
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enter.Invoke();
        }
    }

    /// <summary>
    /// Built-in method called by Unity when an object with a collider attached stay on a trigger collider attached to this.GameObject.
    /// It filters if the collider detected is tagged as player and defines "canInteract" as true if so.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }
    
    /// <summary>
    /// Built-in method called by Unity when an object with a collider attached exits from a trigger collider attached to this.GameObject.
    /// It filters if the collider detected is tagged as player, defines "canInteract" as false if so and triggers "Leave" event.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            Leave.Invoke();
        }
    }

    /// <summary>
    /// It manages if an input was detected and if it refers to KeyCode.Return A.K.A Enter Key.
    /// If an input as described was detected and "canInteract" variable is defined as true
    /// it triggers "Interacted" event.
    /// </summary>
    void CheckForInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canInteract)
        {
            Interacted.Invoke();
        }
    }
}
