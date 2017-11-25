using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour, IDestroyable {
    
    public delegate void ChangeFloat(float val);
    public static event ChangeFloat ChangeMana;

    [SerializeField]
    float sleepTime;
    [SerializeField]
    float absorbRadius;
    [SerializeField]
    float absorbSpeed;
    [SerializeField]
    LayerMask layerMask;

    Rigidbody2D rb2d;
    
    Collider2D hit;

    float timer;
    public float Timer
    {
        get
        {
            return timer;
        }
    }

    int affectedIndex;

    float radians;

    Vector2 velocity;

    float positionX;
    float positionY;

    // Use this for initialization
    void Awake ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), MovementManager.Collider, true);

        Magnet.PullMana += Absorb;

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
            if (hit = Physics2D.OverlapCircle(transform.position, absorbRadius, layerMask))
            {
                Absorb(hit.transform.position);
            }
        }
    }

    void Absorb(Vector2 point)
    {
        positionY = point.y - transform.position.y;
        positionX = point.x - transform.position.x;

        //Get the angle between the mouse and camera in radians.
        radians = (Mathf.Atan2(positionY, positionX));

        velocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * absorbSpeed;

        rb2d.velocity += velocity;
    }

    public void Kill()
    {
        Magnet.PullMana -= Absorb;

        Destroy(gameObject);
    }
}
