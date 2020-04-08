using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour
{
	public GameObject birdPrefab;
	public float firingSensitivity;
	
	private Vector3 initialMousePosition;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true) {
			//get vector of mouse position
			initialMousePosition = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0) == true) {
			//create and fire a bird
			Vector3 firingVector = (initialMousePosition - Input.mousePosition)*firingSensitivity;
			GameObject birdGameObject = Instantiate(birdPrefab, this.transform);
			birdGameObject.transform.position += Vector3.right * 10f;
			
			Rigidbody birdRigidBody = birdGameObject.GetComponent<Rigidbody>();
			birdRigidBody.AddForce(firingVector, ForceMode.Impulse);
		}
    }
}
