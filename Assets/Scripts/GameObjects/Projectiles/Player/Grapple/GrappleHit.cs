using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHit : ProjectileHit
{
    public delegate void TetherAttach(Rigidbody2D rb2d);
    public static event TetherAttach AttachTether;

    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Environment")
        {
            rb2d.velocity = new Vector2(0, 0);

            rb2d.constraints = RigidbodyConstraints2D.FreezePosition;

            AttachTether(rb2d);
        }
    }
}
