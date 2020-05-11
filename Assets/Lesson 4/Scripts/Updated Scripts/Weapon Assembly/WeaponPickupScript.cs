using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupScript : MonoBehaviour
{
    private const int fpLayer = 10;
    private const int weaponLayer = 11;
    Rigidbody rb;
    Collider col;
    Transform[] children;
    MonoBehaviour[] components;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<MeshCollider>();

        components = GetComponents<MonoBehaviour>();
        children = GetComponentsInChildren<Transform>();
    }
    public void Pickup(Transform hudPos)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.SetParent(hudPos, false);
        transform.SetAsFirstSibling();

        foreach(Transform child in children)
        {
            child.gameObject.layer = fpLayer;
        }
        
        foreach (MonoBehaviour script in components)
        {
            script.enabled = true;
        }

        rb.isKinematic = true;
        col.enabled = false;
    }

    public void Drop(float thrust)
    {
        foreach(Transform child in children)
        {
            child.gameObject.layer = weaponLayer;
        }
        
        foreach (MonoBehaviour script in components)
        {
            script.enabled = false;
        }

        rb.isKinematic = false;
        col.enabled = true;

        transform.SetParent(null);

        rb.AddRelativeForce(Vector3.forward * thrust);
    }
}
