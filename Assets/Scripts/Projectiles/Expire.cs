using UnityEngine;
using System.Collections;

public class Expire : MonoBehaviour {

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start ()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(rb2d.velocity == new Vector2(0,0))
        {
            Destroy(gameObject);
        }

	}
}
