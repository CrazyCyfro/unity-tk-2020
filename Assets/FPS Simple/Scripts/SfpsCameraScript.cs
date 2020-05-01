using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfpsCameraScript : MonoBehaviour
{
    public Camera playerCamera;

    private float camVertAngle = 0;

    public float sens;

    void Update()
    {
        UpdateCameraRotation();
        UpdateCursorState();

        // Debug.Log("Cooked: " + Input.GetAxis("Mouse X").ToString("F4") + " Raw: " + Input.GetAxisRaw("Mouse X").ToString("F4"));
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        transform.Rotate(Vector3.up, LookInputX());
  
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
