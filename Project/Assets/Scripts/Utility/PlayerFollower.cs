using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}