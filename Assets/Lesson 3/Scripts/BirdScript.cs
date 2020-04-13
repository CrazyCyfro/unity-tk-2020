using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
	
	public float timeAlive;
	private float createTime;
	
    // Start is called before the first frame update
    void Start()
    {
		createTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - createTime > timeAlive) {
			Destroy(gameObject);
		}
    }
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "wood" || collision.gameObject.tag == "steel") {
			ObjectScript objectScript = collision.gameObject.GetComponent<ObjectScript>();
			objectScript.Hit();
		}
	}
}
