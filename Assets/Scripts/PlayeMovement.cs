using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float speed = 3;

    private PlayerInput playerInput;
    private InputAction moveAction;

    private float rotationX = 0f; // Pitch
    private float rotationY = 0f; // Yaw

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * direction.x + transform.forward * direction.y;
        transform.position += move * speed * Time.deltaTime;
    }

    void RotateCamera()
    {
        // Get mouse input for rotation
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limits vertical look angle

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
