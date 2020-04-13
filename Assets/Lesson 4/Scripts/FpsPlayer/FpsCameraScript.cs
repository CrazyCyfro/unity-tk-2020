using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCameraScript : MonoBehaviour
{
    [Header("Camera settings")]
    public Camera playerCamera;
    private float camVertAngle = 0;

    [Header("Look sensitivity")]
    public float sens;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateCameraRotation();
        UpdateCursorState();
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
}
