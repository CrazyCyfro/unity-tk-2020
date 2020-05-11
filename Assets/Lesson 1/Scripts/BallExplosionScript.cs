using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallExplosionScript : MonoBehaviour
{

	//create a public GameObject for the explosion object
	public GameObject explosion;

	//instructions on creating explosions
	//you've learnt about Start() and Update(), but now you have to use the function OnCollisionEnter(Collision collision), which is called when 2 colliders interact
	//that function, unlike Start() and Update(), returns a collision - read what a collision object is on the Unity website!
	//hint:use collision.collider.tag to check if what you've collided into is the player
	//hint#2: use instantiate to create the explosion (read what that function is on the unity website too!)
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Player") {
			Instantiate(explosion, transform.position, transform.rotation);
		}
	}
}
