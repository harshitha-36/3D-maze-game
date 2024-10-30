using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public Vector3 offset;          // Offset between the player and the camera
    public float smoothSpeed = 0.125f;  // How smooth the camera movement should be

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;  // Where the camera should move to
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // Smooth transition
        transform.position = smoothedPosition;

        transform.LookAt(player);  // Keep looking at the player
    }
}
