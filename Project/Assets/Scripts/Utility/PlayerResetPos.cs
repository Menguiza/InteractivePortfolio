using UnityEngine;

public class PlayerResetPos : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator panelAnim;
    [SerializeField] private AnimationClip easeOut;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = player.position;
        originalRotation = player.rotation;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<WheelController>().DisableMovement();
            panelAnim.SetTrigger("EaseOut");
            Invoke("EaseIn", easeOut.length);
        }
    }

    void EaseIn()
    {
        player.position = originalPosition;
        player.rotation = originalRotation;
        player.GetComponent<WheelController>().EnableMovement();
        panelAnim.SetTrigger("EaseIn");
    }
}
