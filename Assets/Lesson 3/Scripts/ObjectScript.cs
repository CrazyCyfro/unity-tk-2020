using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
	
	public GameDataScriptableObject gameData;
	private int hitsToDestroy;
	private int currentHits = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "wood") {
			hitsToDestroy = gameData.hitsToDestroyWood;
		}
		else if (gameObject.tag == "steel") {
			hitsToDestroy = gameData.hitsToDestroySteel;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y<-10) {//so object is destroyed after falling off the platform
			Destroy(gameObject);
		}
    }
	
	private void Hit() {
		currentHits++;
		//print(((float)currentHits/(float)hitsToDestroy*0.1f).ToString());
		gameObject. GetComponent<Renderer>().material.color = new Color((float)currentHits/(float)hitsToDestroy,0,0);
		if (currentHits == hitsToDestroy) {
			if (gameObject.tag == "wood") {
				gameData.score += 1;
			}
			else if (gameObject.tag ==  "steel") {
				gameData.score += 2;
			}
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "bird") {
			Hit();
		}
	}
}
