using UnityEngine;

/// <summary>
/// This class manages GameObject's position to follow an specific transform.
/// In this case the component transform to follow is the one attached our player.
/// </summary>
public class PlayerFollower : MonoBehaviour
{
    //Assignable parameters
    [SerializeField] private Transform player;
    
    void Update()
    {
        //Method called to update constantly GameObject's position
        Follow();
    }

    /// <summary>
    /// Takes reference transform position and create a new Vector3 with each axis value.
    /// Then it applies the result Vector3 to this object transform.position.
    /// </summary>
    private void Follow()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}