using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfpsMovementScript : MonoBehaviour
{
    public SimplePlayerData playerData;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float yVelocity;
    public float moveSpeed;
    void Update()
    {
        controller.Move((UpdatePlayerMovement() + UpdatePlayerGravity()) * Time.deltaTime);
        playerData.playerPos = transform.position;
    }

    Vector3 UpdatePlayerGravity()
    {
        if (Physics.Raycast(groundCheck.position, Vector3.down, 1.1f, groundLayer)) {
            yVelocity = -0.01f;
        } else {
            yVelocity += -9.81f * Time.deltaTime;
        }

        return new Vector3(0, yVelocity, 0);
    }
    Vector3 UpdatePlayerMovement()
    {
        Vector3 motion = transform.right * MoveInputX() + transform.forward * MoveInputY();
        
        if (motion.magnitude > 1) motion.Normalize();

        return motion * moveSpeed;
    }

    float MoveInputX() {
        return Input.GetAxis("Horizontal");
    }

    float MoveInputY() {
        return Input.GetAxis("Vertical");
    }
}
