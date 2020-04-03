using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

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
}
