using UnityEngine;
using System.Collections;

public class PhysicsMove : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    public float jumpDecay;

    float tempJumpSpeed;
    bool floored = true;
    Rigidbody2D rb2d;

	// Use this for initialization
	void Start ()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        ////Store the current horizontal input in the float moveHorizontal.
        //float moveHorizontal = Input.GetAxis("Horizontal");

        ////Store the current vertical input in the float moveVertical.
        //float moveVertical = Input.GetAxis("Vertical");

        ////Use the two store floats to create a new Vector2 variable movement.
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //if(movement != new Vector2(0,0))
        //{
        //    int temp = 0;
        //}
        ////Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //rb2d.AddForce(movement * speed);

        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddForce(new Vector2(-1, 0) * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(new Vector2(1, 0) * speed);
        }
        
        if ((Input.GetKeyDown(KeyCode.Space) && floored) || (Input.GetKey(KeyCode.Space) && !floored))
        {
            rb2d.AddForce(new Vector2(0, 1) * tempJumpSpeed);

            if(tempJumpSpeed > 0)
            {
                tempJumpSpeed -= jumpDecay;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) && !floored)
        {
            tempJumpSpeed = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            tempJumpSpeed = jumpSpeed;

            floored = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            floored = false;
        }

        if(tempJumpSpeed == jumpSpeed)
        {
            tempJumpSpeed = 0;
        }
    }
}
