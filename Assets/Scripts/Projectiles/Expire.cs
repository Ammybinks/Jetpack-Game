using UnityEngine;

public class Expire : MonoBehaviour {

    public float velocityCutoff;
    public float scaleCutoff;
    public float timeCutoff;
    public bool expireOnVelocity = false;
    public bool expireOnScale = false;
    public bool expireOnTime = false;

    Vector2 tempVelocity = new Vector2();
    Vector2 tempScale = new Vector2();
    float timer;

    Rigidbody2D rb2d;
    
    // Use this for initialization
    void Start ()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(expireOnVelocity)
        {
            tempVelocity = rb2d.velocity;

            if (tempVelocity.x < 0)
            {
                tempVelocity.x *= -1;
            }

            if (tempVelocity.y < 0)
            {
                tempVelocity.y *= -1;
            }

            if (tempVelocity.x <= velocityCutoff && tempVelocity.y <= velocityCutoff)
            {
                Destroy(gameObject);
            }
        }

        if(expireOnScale)
        {
            tempScale = transform.localScale;

            if(tempScale.x >= scaleCutoff || tempScale.y >= scaleCutoff)
            {
                Destroy(gameObject);
            }
        }

        if(expireOnTime)
        {

        }
	}
}
