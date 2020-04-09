using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPlayerScript : MonoBehaviour
{

    public CharacterController controller;

    public Transform groundChecker;

    // Look sensitivity
    public float sens;
    // Movement speed multiplier
    public float moveSpeed;

    private Vector3 gravVector;

    public float dist2Ground;

    public LayerMask groundMask;

    public float GRAVITY = -9.81f;
    public Camera playerCamera;

    private float camVertAngle = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateCameraRotation();
        UpdateCursorState();
        
        controller.Move(UpdatePlayerMove() + UpdateGravVelocity() * Time.deltaTime);
    }

    Vector3 UpdateGravVelocity()
    {

        if (Physics.Raycast(groundChecker.position, Vector3.down, dist2Ground, groundMask)) {
            gravVector = new Vector3(0, -0.001f, 0);
        } else {
            gravVector.y += GRAVITY * Time.deltaTime;
        }

        return gravVector;
    }
    Vector3 UpdatePlayerMove()
    {
        Vector3 moveVector = transform.right * MoveInputX() + transform.forward * MoveInputY();

        // Clamp magnitude to 1
        if (moveVector.magnitude > 1) moveVector.Normalize();

        return moveVector * moveSpeed * Time.deltaTime;
    }

    void UpdateCursorState()
    {
        // Lock on Left Click
        if (Input.GetMouseButtonDown(0)) Cursor.lockState = CursorLockMode.Locked;
        
        // Unlock on Escape
        if (Input.GetKeyDown(KeyCode.Escape)) Cursor.lockState = CursorLockMode.None;
    }

    void UpdateCameraRotation()
    {
        // Horizontal rotation
        transform.Rotate(new Vector3(0, LookInputX(), 0));

        // Vertical rotation
        camVertAngle += LookInputY();
        camVertAngle = Mathf.Clamp(camVertAngle, -89f, 89f);
        playerCamera.transform.localEulerAngles = new Vector3(-camVertAngle, 0, 0); 

    }

    float LookInputX() {
        return Input.GetAxisRaw("Mouse X") * sens;
    }

    float LookInputY() {
        return Input.GetAxisRaw("Mouse Y") * sens;
    }

    float MoveInputX() {
        return Input.GetAxis("Horizontal");
    }

    float MoveInputY() {
        return Input.GetAxis("Vertical");
    }
}
