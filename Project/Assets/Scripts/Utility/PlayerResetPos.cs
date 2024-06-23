using UI;
using UnityEngine;
using Utility.GameFlow;

/// <summary>
/// This class manages player resetting position when the playable area is exceeded.
/// </summary>
[RequireComponent (typeof(BoxCollider))]
public class PlayerResetPos : MonoBehaviour
{
    //Assignable parameters
    [Header("Player Related")]
    [SerializeField] private Transform player;

    [Header("Feedback Related")]
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private AnimationClip easeOut;

    //Utility parameters
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private BoxCollider transitableArea;

    private void Awake()
    {
        transitableArea = GetComponent<BoxCollider>();

        GameManager.Pause += ToggleTransitableArea;
    }

    void Start()
    {
        //Setting original position
        originalPosition = player.position;
        originalRotation = player.rotation;
    }

    /// <summary>
    /// Built-in method called by Unity when an object with a collider attached exits a trigger collider attached to this.GameObject.
    /// It filters if the collider detected is tagged as player.
    /// If so, it'll call "DisableMovement" method defined on the "WheelController" component attached to the player.
    /// Then it'll activate "EaseOut" trigger of an animator ("panelAnim" in this case) and Invoke "EaseIn" method with an
    /// animationClip ("easeOut" in this case) length as delay.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<WheelController>().DisableMovement();

            fadePanel?.TriggerFade(FadeType.EaseOut);

            Invoke("EaseIn", easeOut.length);
        }
    }

    /// <summary>
    /// Returns player to it's original position by assigning a Vector3 variable to player's transform.position ("originalPosition" in this case).
    /// Then it'll call "EnableMovement" method defined on the "WheelController" component attached to the player and  will activate "EaseOut"
    /// trigger of an animator ("panelAnim" in this case)
    /// </summary>
    private void EaseIn()
    {
        player.position = originalPosition;
        player.rotation = originalRotation;

        player.GetComponent<WheelController>().EnableMovement();

        fadePanel?.TriggerFade(FadeType.EaseIn);
    }

    private void ToggleTransitableArea(bool isInactive)
    {
        transitableArea.enabled = !isInactive;
    }
}
