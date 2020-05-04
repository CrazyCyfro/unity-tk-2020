using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallSpawnerScript : MonoBehaviour
{
    public GameObject ballPrefab;
    public MeshRenderer planeMeshRenderer;
    public MeshFilter planeMeshFilter;

    public float ballSpawnInterval;
    public float forceMultiplier;
    public float ballLifespan;

    const int absoluteSize = 10;
    Vector3 planeScale;
    float t = 0;
    Vector3 xSpawn;
    Vector3 ySpawn;
    Vector3 spawnLocation;
    

    

    // Start is called before the first frame update
    void Start()
    {
        t = Time.time;
        planeScale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > t)
        {
            t += ballSpawnInterval;
            xSpawn = planeMeshRenderer.transform.right * Random.Range(-planeScale.x/2,planeScale.x/2);
            ySpawn = planeMeshRenderer.transform.forward * Random.Range(-planeScale.z/2,planeScale.z/2);
            spawnLocation = planeMeshRenderer.transform.position + (xSpawn + ySpawn) * absoluteSize;
            
            
            GameObject ball =  Instantiate(ballPrefab, spawnLocation, Quaternion.identity);
            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            
            BallScript ballScript = ball.GetComponent<BallScript>();
            ballScript.SetLifespan(ballLifespan);
            
            Vector3 normal = planeMeshFilter.transform.TransformDirection(planeMeshFilter.mesh.normals[0]);

            ballRb.AddForce(normal * forceMultiplier, ForceMode.Impulse);
            
    
        }
    }
}
