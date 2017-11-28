using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour {

    public delegate BoxCollider2D[] GetCollider();
    public static event GetCollider RequestCollider;
    
    [SerializeField]
    float sleepTime;
    [SerializeField]
    float absorbSpeed;

    Rigidbody2D rb2d;

    BoxCollider2D collider;

    BoxCollider2D[] playerColliders;

    float timer;

    int affectedIndex;

    float radians;

    Vector2 velocity;

    float positionX;
    float positionY;

    // Use this for initialization
    void Awake ()
    {
        rb2d = GetComponent<Rigidbody2D>();

        collider = GetComponent<BoxCollider2D>();
        if(RequestCollider != null)
        {
            playerColliders = RequestCollider();
        }

        foreach(BoxCollider2D playerCollider in playerColliders)
        {
            Physics2D.IgnoreCollision(collider, playerCollider);
        }

        ManaManager.PullMana += Absorb;

        timer = sleepTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (playerColliders != null)
            {
                foreach (BoxCollider2D playerCollider in playerColliders)
                {
                    Physics2D.IgnoreCollision(collider, playerCollider, false);
                }

                playerColliders = null;
            }
        }
	}

    private void Absorb(Vector2 point, ArrayList affected)
    {
        if ((affectedIndex = affected.IndexOf(name)) != -1)
        {
            positionY = point.y - transform.position.y;
            positionX = point.x - transform.position.x;

            //Get the angle between the mouse and camera in radians.
            radians = (Mathf.Atan2(positionY, positionX));

            velocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * absorbSpeed;

            rb2d.velocity += velocity;

            affected.RemoveAt(affectedIndex);
        }
    }
}
