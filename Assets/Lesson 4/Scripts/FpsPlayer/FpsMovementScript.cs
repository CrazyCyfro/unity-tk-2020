using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovementScript : MonoBehaviour
{

    public CharacterController controller;
    public PlayerData playerData;

    [Header("Move speed multiplier")]
    public float moveSpeed;

    [Header("Gravity settings")]
    public Transform groundChecker;
    private Vector3 gravVector;
    public float dist2Ground;
    public LayerMask groundMask;
    public float GRAVITY = -9.81f;

    void Update()
    {
        controller.Move(UpdatePlayerMove() + UpdateGravVelocity() * Time.deltaTime);
        UpdatePlayerTransform();
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

    void UpdatePlayerTransform()
    {
        playerData.playerPos = transform.position;
    }

    float MoveInputX() {
        return Input.GetAxis("Horizontal");
    }

    float MoveInputY() {
        return Input.GetAxis("Vertical");
    }
}
