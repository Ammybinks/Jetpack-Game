using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour, IDestroyable
{
    public float damage;
    public float iFrames;

    public bool killOnCollide;
    public LayerMask killMask;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(killOnCollide && killMask != (killMask | (1 << collision.gameObject.layer)))
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
