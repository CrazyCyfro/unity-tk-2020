using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LauncherScript : MonoBehaviour
{
	public GameObject birdPrefab;
	public GameObject camera;
	public GameObject firepowerTracker;
	public GameObject birdsCreatedTracker;
	
	public float firingSensitivity;
	public float maxTimeToHoldFire;
	public float fireInterval;
	public int maxBirdsToCreate;
	
	private float initialTime = 0;
	private float fireTiming = -1;
	private int birdsCreated = 0;
	private Animator firepowerTrackerAnimator;
	
    // Start is called before the first frame update
    void Start()
    {
        firepowerTrackerAnimator = firepowerTracker.GetComponent<Animator>();
		firepowerTrackerAnimator.speed = (1/maxTimeToHoldFire);
		birdsCreatedTracker.GetComponent<TextMeshProUGUI>().text = "Birds remaining: " + (maxBirdsToCreate - birdsCreated).ToString() + "/" + maxBirdsToCreate.ToString();
    }

    // Update is called once per frame
    void Update()
    {
		if (canFire()) {
			if (Input.GetMouseButtonDown(0) == true) {
				//get vector of mouse position
				initialTime = Time.time;
				firepowerTrackerAnimator.SetBool("isFiring", true);
			}
			else if (Input.GetMouseButtonUp(0) == true) {
				//create and fire a bird
				GameObject birdGameObject = Instantiate(birdPrefab, transform.position, transform.rotation);
				birdGameObject.transform.position += transform.forward * 3f;
				
				float firingForceMultiplier;
				if (Time.time-initialTime > maxTimeToHoldFire) {
					firingForceMultiplier = maxTimeToHoldFire * firingSensitivity;
				}
				else {
					firingForceMultiplier = (Time.time - initialTime) * firingSensitivity;
				}
				
				Vector3 firingVector = transform.position - camera.transform.position;
				
				fireTiming = Time.time;
				firepowerTrackerAnimator.SetBool("isFiring", false);
				firepowerTrackerAnimator.Play("Empty");
				
				birdsCreated++;
				birdsCreatedTracker.GetComponent<TextMeshProUGUI>().text = "Birds remaining: " + (maxBirdsToCreate - birdsCreated).ToString() + "/" + maxBirdsToCreate.ToString();
				
				Rigidbody birdRigidBody = birdGameObject.GetComponent<Rigidbody>();
				birdRigidBody.AddForce(firingVector * firingForceMultiplier, ForceMode.Impulse);
			}
		}
    }
	
	bool canFire() {
		if (((Time.time - fireTiming) > fireInterval) && (birdsCreated < maxBirdsToCreate)) {
			return true;
		}
		else {
			return false;
		}
	}
}
