using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupScript : MonoBehaviour
{
    public int[] slots;
    private float thrust;
    private const int FP_LAYER = 10;
    private const int WEAPON_LAYER = 11;
    Rigidbody rb;
    Collider col;
    Transform[] children;
    MonoBehaviour[] components;
    void Start()
    {
        thrust = 1;

        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<MeshCollider>();

        components = GetComponents<MonoBehaviour>();
        children = GetComponentsInChildren<Transform>();
    }
    public WeaponPickupScript Pickup()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        foreach(Transform child in children)
        {
            child.gameObject.layer = FP_LAYER;
        }
        
        foreach (MonoBehaviour script in components)
        {
            script.enabled = true;
        }

        rb.isKinematic = true;
        col.enabled = false;

        return this;
    }

    public void Drop(Vector3 dropOffPoint)
    {
        transform.SetParent(null);
        transform.position = dropOffPoint;

        foreach(Transform child in children)
        {
            child.gameObject.layer = WEAPON_LAYER;
        }
        
        foreach (MonoBehaviour script in components)
        {
            script.enabled = false;
        }

        rb.isKinematic = false;
        col.enabled = true;

        rb.AddRelativeForce(Vector3.up * thrust);
    }
}
