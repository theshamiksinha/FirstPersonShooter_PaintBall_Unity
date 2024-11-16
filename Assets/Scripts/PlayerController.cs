using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of camera movement
    public float lookSpeed = 2f;  // Speed of camera rotation

    private float pitch = 0f;  // For up/down rotation
    private float yaw = 0f;    // For left/right rotation

    void Update()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); // For A and D keys
        float moveZ = Input.GetAxis("Vertical");   // For W and S keys

        // Calculate movement direction, but zero out any movement in the Y axis
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection.y = 0f; // Prevent movement in the Y axis

        // Apply the movement to the camera
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Get mouse movement for looking around
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Adjust yaw (left-right) and pitch (up-down) based on mouse input
        yaw += mouseX;
        pitch -= mouseY;

        // Clamp pitch to prevent flipping the camera upside down
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Apply rotation to the camera
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}
