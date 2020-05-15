using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LauncherScript : MonoBehaviour
{
	public GameObject birdPrefab;
	public GameObject cam;
	public GameObject firepowerTracker;
	public GameObject birdsCreatedTracker;
	
	public float firingSensitivity;
	public float maxTimeToHoldFire;
	public float fireInterval;
	public int maxBirdsToCreate;
	
	private float initialTime = 0;
	private float fireTiming = -1;
	private int birdsCreated = 0;
	private bool isFiring = false;
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
				isFiring = true; //so that it doesnt skip the getmousebuttondown step
			}
			else if (Input.GetMouseButtonUp(0) == true && isFiring) {
				//create and fire a bird
				GameObject birdGameObject = Instantiate(birdPrefab, transform.position, transform.rotation);
				birdGameObject.transform.position -= transform.forward * 1f;
				birdGameObject.transform.position += transform.up * 1f;
				
				float firingForceMultiplier;
				print("Current Time: " + Time.time.ToString());
				print("Init: " + initialTime.ToString());
				if (Time.time-initialTime > maxTimeToHoldFire) {
					firingForceMultiplier = maxTimeToHoldFire * firingSensitivity;
				}
				else {
					firingForceMultiplier = (Time.time - initialTime) * firingSensitivity;
				}
				
				Vector3 firingVector = transform.position - cam.transform.position;
				firingVector += transform.up * 10f;
				
				fireTiming = Time.time;
				firepowerTrackerAnimator.SetBool("isFiring", false);
				firepowerTrackerAnimator.Play("Empty");
				
				birdsCreated++;
				birdsCreatedTracker.GetComponent<TextMeshProUGUI>().text = "Birds remaining: " + (maxBirdsToCreate - birdsCreated).ToString() + "/" + maxBirdsToCreate.ToString();
				
				Rigidbody birdRigidBody = birdGameObject.GetComponent<Rigidbody>();
				birdRigidBody.AddForce(firingVector * firingForceMultiplier, ForceMode.Impulse);
				
				if (birdsCreated == maxBirdsToCreate) {
					EndGame();
				}
				
				isFiring = false;
			}
		}
		if (GameObject.FindGameObjectsWithTag("wood").Length == 0 && GameObject.FindGameObjectsWithTag("steel").Length == 0) {
			EndGame();
		}
		if (Input.GetKeyDown("q")) {
			EndGame();
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
	
	void EndGame() {
		SceneManager.LoadScene("EndGameScene");
	}
}
