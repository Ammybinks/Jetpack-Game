using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public Rigidbody2D projectile;
    public int speed;
    public int recoil;
    public int maxRecoil;
    
    Camera mainCamera;

    Vector3 rotation;
    Vector3 mousePosition;

    Vector2 tempVelocity;

    Rigidbody2D bulletInstance;

    float mousePositionY;
    float mousePositionX;
    float degrees;
    float radians;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            rotation = transform.eulerAngles;
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            mousePositionY = mousePosition.y - transform.position.y;
            mousePositionX = mousePosition.x - transform.position.x;

            transform.eulerAngles = rotation;

            bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

            degrees = (Mathf.Atan2(mousePositionY, mousePositionX) * (180 / Mathf.PI));

            radians = degrees * Mathf.Deg2Rad;

            tempVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * speed;

            bulletInstance.velocity = tempVelocity;
            bulletInstance.rotation = degrees;

            tempVelocity *= -1;

            if(tempVelocity.y < maxRecoil)
            {
                
            }
        }
    }
}
