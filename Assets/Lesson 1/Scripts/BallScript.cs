using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

	//create a public GameObject for the explosion object
	public GameObject explosion;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
     
    }


    void LateUpdate() {
        if (Time.time > t) {
            Destroy(gameObject);
        }
    }

    public void SetLifespan(float lifespan) {
        t = Time.time + lifespan;
    }
	
	
	//instructions on creating explosions
	//you've learnt about Start() and Update(), but now you have to use the function OnCollisionEnter(Collision collision), which is called when 2 colliders interact
	//that function, unlike Start() and Update(), returns a collision - read what a collision object is on the Unity website!
	//hint:use collision.collider.tag to check if what you've collided into is the player
	//hint#2: use instantiate to create the explosion (read what that function is on the unity website too!)
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Player") {
			Instantiate(explosion, transform);
		}
	}
}
