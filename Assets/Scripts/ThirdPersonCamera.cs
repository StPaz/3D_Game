using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The target the camera follows
    public float distance = 5f; // Distance from the target
    public float height = 2f; // Height above the target
    public float smoothSpeed = 5f; // Smoothing factor for camera movement
    public float rotationSpeed = 5f; // Mouse rotation speed

    private Vector3 offset; // Offset between the camera and the target
    private float currentRotationX = 0f; // Current camera rotation around the target on the X-axis
    private float currentRotationY = 0f; // Current camera rotation around the target on the Y-axis

    void Start()
    {
        // Calculate the initial offset between the camera and the target
        offset = new Vector3(0f, height, -distance);
    }

    void LateUpdate()
    {
        // Check if the target is assigned
        if (target != null)
        {
            // Mouse rotation (horizontal and vertical)
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            currentRotationX += mouseX;
            currentRotationY -= mouseY;

            // Limit the vertical camera rotation to stay within reasonable angles
            currentRotationY = Mathf.Clamp(currentRotationY, -90f, 90f);

            // Calculate the desired camera rotation
            Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);

            // Calculate the desired camera position
            Vector3 desiredPosition = target.position + rotation * offset;

            // Smoothly move the camera to the desired position using Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Apply the smoothed position to the camera
            transform.position = smoothedPosition;

            // Make the camera look at the target
            transform.LookAt(target);
        }
    }
}
