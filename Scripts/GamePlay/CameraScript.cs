using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;  // The player object to follow
    public float distance = 5.0f; // Distance from the player
    public float rotationSpeed = 5.0f; // Speed of camera rotation
    public float height = 2.0f; // Height above the player

    private float currentAngle; // Current rotation angle

    void Start()
    {
        // Set the initial angle based on the current rotation
        currentAngle = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Get mouse input for rotation
        float horizontalInput = Input.GetAxis("Mouse X");
        currentAngle += horizontalInput * rotationSpeed;

        // Calculate the new position and rotation of the camera
        Vector3 newPosition = player.position - new Vector3(0, 0, distance);
        newPosition.y += height; // Set the height
        transform.position = newPosition;

        // Rotate the camera to look at the player
        transform.LookAt(player.position + Vector3.up * height); // Look at the player with height offset
        transform.RotateAround(player.position, Vector3.up, currentAngle); // Rotate around the player
    }
}
