using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float jumpDecay;
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    float raySize;

    static MovementManager instance;

    private Collider2D collider;
    public static Collider2D Collider
    {
        get
        {
            return instance.collider;
        }
    }

    float tempJumpSpeed;
    float defaultDrag;

    bool leftKeyDown;
    bool rightKeyDown;
    bool jumpKeyDown;

    internal bool floored = true;
    internal bool tethered = false;

    HingeJoint2D hinge;

    Rigidbody2D rb2d;

    Vector2 realVelocity;

    RaycastHit2D hit;

    public static Vector2 movementSpeed
    {
        get
        {
            return instance.GetComponent<Rigidbody2D>().velocity;
        }
    }

    // Use this for initialization
    void Awake ()
    {
        instance = this;

        collider = GetComponent<Collider2D>();

        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        defaultDrag = rb2d.drag;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update()
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

        ResetKeys();

        if (Input.GetKey(KeyCode.A) && floored && !tethered)
        {
            leftKeyDown = true;
        }

        if (Input.GetKey(KeyCode.D) && floored && !tethered)
        {
            rightKeyDown = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpKeyDown = false;
        }

        hit = Physics2D.Raycast(transform.position, -transform.up, raySize, layerMask);
        Debug.DrawRay(transform.position, new Vector2(0, -raySize), Color.red);
        if (hit)
        {
            if (hit.transform.gameObject.tag == "Environment" && !floored)
            {
                jumpKeyDown = false;

                tempJumpSpeed = jumpSpeed;

                floored = true;
            }
        }
        else
        {
            if (floored)
            {
                floored = false;

                if (tempJumpSpeed == jumpSpeed)
                {
                    tempJumpSpeed = 0;
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (leftKeyDown)
        {
            rb2d.AddForce(new Vector2(-1, 0) * speed);
        }

        if (rightKeyDown)
        {
            rb2d.AddForce(new Vector2(1, 0) * speed);
        }

        if (jumpKeyDown)
        {
            rb2d.AddForce(new Vector2(0, 1) * tempJumpSpeed);

            if (tempJumpSpeed > 0)
            {
                tempJumpSpeed -= jumpDecay;
            }
        }

        //if(!floored)
        //{
        //    rb2d.drag = 0;
        //}
        //else
        //{
        //    rb2d.drag = defaultDrag;
        //}

        if (!jumpKeyDown && !floored)
        {
            tempJumpSpeed = 0;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        TempJumpSpeed = JumpSpeed;

    //        Floored = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        Floored = false;

    //        if (TempJumpSpeed == JumpSpeed)
    //        {
    //            TempJumpSpeed = 0;
    //        }
    //    }
    //}

    private void ResetKeys()
    {
        leftKeyDown = false;
        rightKeyDown = false;
    }

    public static void AttachTether(Rigidbody2D rb2d)
    {
        instance.hinge = instance.gameObject.AddComponent<HingeJoint2D>();

        instance.hinge.connectedBody = rb2d;

        instance.tethered = true;
    }

    public static void BreakTether()
    {
        Destroy(instance.hinge);

        instance.tethered = false;
    }

    public static void Accelerate(Vector2 velocity)
    {
        instance.rb2d.velocity += velocity;
    }
}
