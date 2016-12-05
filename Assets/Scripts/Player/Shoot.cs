using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public Rigidbody2D projectile;
    public float speed;
    public float recoil;
    public float maxRecoil;
    
    Camera mainCamera;
    
    Vector3 mousePosition;

    Vector2 playerVelocity;
    Vector2 bulletVelocity;
    Vector2 recoilVelocity;

    Rigidbody2D bulletInstance;
    Rigidbody2D rb2d;

    float mousePositionY;
    float mousePositionX;
    float degrees;
    float radians;

    void Awake()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        //Get and store a reference to the main camera.
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        //If the left mouse button is held:
        if (Input.GetMouseButton(0))
        {
            //Save the current position of the mouse in relation to the main camera.
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            mousePositionY = mousePosition.y - transform.position.y;
            mousePositionX = mousePosition.x - transform.position.x;
            
            //Create an instance of the projectile.
            bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

            //Get the angle between the mouse and camera in degrees.
            degrees = (Mathf.Atan2(mousePositionY, mousePositionX) * (180 / Mathf.PI));

            //Convert the angle between the mouse and camera into radians.
            radians = degrees * Mathf.Deg2Rad;

            //Create the velocity of the projectile.
            bulletVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * speed;
            recoilVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (recoil * -1);

            //Set the projectile's velocity and direction.
            bulletInstance.velocity = bulletVelocity;
            bulletInstance.rotation = degrees;
            
            //Store rigidBody2D's velocity in a local variable.
            playerVelocity = rb2d.velocity;

            //If current y velocity is less than the maximum recoil can add to the velocity:
            if(playerVelocity.y < maxRecoil)
            {
                //Add the recoil velocity to current velocity.
                playerVelocity.y += recoilVelocity.y;

                //If current y velocity now exeeds the maximum recoil, reduce velocity to maximum recoil velocity.
                if(playerVelocity.y > maxRecoil)
                {
                    playerVelocity.y = maxRecoil;
                }
            }

            //If current x velocity is less than the maximum recoil can add to the velocity:
            if (playerVelocity.x < maxRecoil)
            {
                //Add the recoil velocity to current velocity.
                playerVelocity.x += recoilVelocity.x;

                //If current x velocity now exeeds the maximum recoil, reduce velocity to maximum recoil velocity.
                if (playerVelocity.x > maxRecoil)
                {
                    playerVelocity.x = maxRecoil;
                }
            }

            rb2d.velocity = playerVelocity;
        }
    }
}
