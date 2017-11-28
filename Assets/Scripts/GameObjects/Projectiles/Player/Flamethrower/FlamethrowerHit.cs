using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerHit : ProjectileHit
{

    [SerializeField]
    float cutoff;
    [SerializeField]
    float XExpand;
    [SerializeField]
    float YExpand;

    Vector2 tempScale = new Vector2();

    // Update is called once per frame
    void Update()
    {
        tempScale = gameObject.transform.localScale;

        tempScale.x += XExpand;
        tempScale.y += YExpand;

        gameObject.transform.localScale = tempScale;

        tempScale = transform.localScale;

        if (tempScale.x >= cutoff || tempScale.y >= cutoff)
        {
            Kill();
        }
    }
}
