using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages bowling equipment resetting.
/// </summary>
public class ResetBowling : MonoBehaviour
{
    //Assignable parameters
    [SerializeField] private List<Transform> equipment = new List<Transform>();
    
    //Utility parameters
    private Vector3[] initialPos;
    private Quaternion[] initialRot;
    private Rigidbody[] rb;
    private int index;

    private void Awake()
    {
        //Initialize each array
        initialPos = new Vector3[equipment.Count];
        initialRot = new Quaternion[equipment.Count];
        rb = new Rigidbody[equipment.Count];
    }

    private void Start()
    {
        //Call method to fill arrays
        OriginalSetUp();
    }

    /// <summary>
    /// Fills initialPos array with each element on equipment list position.
    /// Fills initialRot array with each element on equipment list rotation.
    /// Trys to get a rb component attached to each element on equipment list
    /// and add it's reference to rb array.
    /// </summary>
    private void OriginalSetUp()
    {
        foreach (Transform element in equipment)
        {
            initialPos[index] = element.position;
            initialRot[index] = element.rotation;
            element.TryGetComponent(out rb[index]);
            index++;
        }
    }
    
    /// <summary>
    /// Loops through each transform on equipment list and sets it's position and rotation
    /// with the ones defined on initialPos and initialRot arrays.
    /// Then calls a method to prevent rigidbody to move when reset.
    /// </summary>
    public void Reset()
    {
        index = 0;
        
        foreach (Transform element in equipment)
        {
            element.position = initialPos[index];
            element.rotation = initialRot[index];
            StopRigidbody(rb[index]);
            index++;
        }
    }

    /// <summary>
    /// Receive a rigidbody and sets it's velocity to zero.
    /// Then set it as kinematic to prevent any residual forces applied and immediately
    /// return it to a dynamic state. 
    /// </summary>
    /// <param name="rb"> Used as reference to apply certain conditions. </param>
    private void StopRigidbody(Rigidbody rb)
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.isKinematic = false;
    }
}
