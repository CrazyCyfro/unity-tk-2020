using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPlayerScript : MonoBehaviour
{
    public CharacterController controller;
    public PlayerData playerData;
    public AudioSource gunAudio;
    public AudioSource musicAudio;


    [Header("Look sensitivity")]
    public float sens;
    
    [Header("Move speed multiplier")]
    public float moveSpeed;

    [Header("Health settings")]
    public int health;

    [Header("Firing settings")]
    public float fireRate;
    public float bulletForceMultiplier;
    public int bulletDamage;
    private float t = 0;
    private float fireInterval;

    [Header("Bullet impact settings")]
    public GameObject bulletImpactPrefab;

    [Header("Gravity settings")]
    public Transform groundChecker;
    private Vector3 gravVector;
    public float dist2Ground;
    public LayerMask groundMask;
    public float GRAVITY = -9.81f;
    
    [Header("Camera settings")]
    public Camera playerCamera;
    private float camVertAngle = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        fireInterval = 1/fireRate;
    }

    void Update()
    {
        UpdateCameraRotation();
        UpdateCursorState();

        if (Input.GetButtonDown("Fire1") && CanShoot()) {
            Shoot();
        }
        
        controller.Move(UpdatePlayerMove() + UpdateGravVelocity() * Time.deltaTime);
        UpdatePlayerTransform();
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        Debug.Log("Health: " + health);
        
        if (health > 0) return;

        Debug.Log("You lose!");
    }
    
    bool CanShoot()
    {
        if (Time.time - t > fireInterval) {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }

    void Shoot()
    {
        gunAudio.Play();

        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity)) return;

        GameObject particles = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);

        if (hit.collider.gameObject.tag != "Zombie") return;

        ZombieScript zombie = hit.collider.gameObject.GetComponent<ZombieScript>();
        zombie.TakeDamage(bulletDamage, -hit.normal * bulletForceMultiplier, hit.point);
    }

    void UpdatePlayerTransform()
    {
        playerData.playerPos = transform.position;
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
