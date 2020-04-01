using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public InputManagerScript inputManager;
    public Camera playerCamera;

    public Rigidbody playerRb;

    public float movementSpeed;

    float cameraVerticalAngle = 0;
    // float cameraHorizontalAngle = 0;


    
    
    void Update() {
        RotateCamera();
        Movement();
    }

    void Movement() {
        Vector3 targetVelocity = transform.TransformDirection(new Vector3(inputManager.GetMovementInputHorizontal(),0,inputManager.GetMovementInputVertical())*movementSpeed);
        targetVelocity.y = playerRb.velocity.y;
        playerRb.velocity = targetVelocity;
    }



    void RotateCamera() {
         
        // horizontal camera rotation
        // rotate the transform with the input speed around its local Y axis
        playerRb.transform.Rotate(new Vector3(0, inputManager.GetLookInputsHorizontal(), 0));

        
        // vertical camera rotation
        // add vertical inputs to the camera's vertical angle
        cameraVerticalAngle += inputManager.GetLookInputsVertical();

        // limit the camera's vertical angle to min/max
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
        playerCamera.transform.localEulerAngles = new Vector3(-cameraVerticalAngle, 0, 0);
        

    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Ball(Clone)") {
            Debug.Log("YOU LOSE");
        }
    }
}
