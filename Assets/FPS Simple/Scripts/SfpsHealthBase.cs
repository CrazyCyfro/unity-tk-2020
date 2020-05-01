using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SfpsHealthBase : MonoBehaviour
{
    protected int health;
    public virtual bool Dead()
    {
        return health <= 0;
    }
    public virtual void TakeDamage(int damage)
    {
        if (Dead()) return;
        
        health -= damage;
        
        if (Dead()) Die();
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
