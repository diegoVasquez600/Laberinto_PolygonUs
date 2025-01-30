using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate()
    {
        offset.z = -5;
        transform.position = player.position + offset;
    }
}
