using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLifespanScript : MonoBehaviour
{
	float t = 0;
	
    void LateUpdate() {
        if (Time.time > t) {
            Destroy(gameObject);
        }
    }

    public void SetLifespan(float lifespan) {
        t = Time.time + lifespan;
    }
}
