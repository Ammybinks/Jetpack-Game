using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orber : Enemy
{
    float radian;

    Vector2 offSet;

    SpriteRenderer renderer;
    Color defaultColor;

    float rotation;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        defaultColor = renderer.color;

        projectileWidth = projectile.GetComponent<SpriteRenderer>().bounds.size.x / 2;

        StartUp();
    }

    private void Update()
    {
        if (lineOfSight = CheckLoS(transform.position, target.position, (transform.right * (projectileWidth * 1.1f))))
        {
            renderer.color = defaultColor;
        }
        else
        {
            renderer.color = Color.grey;
        }


        rotation = ((Mathf.Atan2(target.position.x - transform.position.x, target.position.y - transform.position.y) * Mathf.Rad2Deg) * -1) + 180;

        Vector3 temp = transform.rotation.eulerAngles;

        temp.z = rotation;

        //Get the angle between the mouse and camera in radians.
        transform.rotation = Quaternion.Euler(temp);

        if (lineOfSight && tempCooldown == 0)
        {
            //Create an instance of the projectile.
            projectileInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

            //Create the velocity of the projectile.
            projectileInstance.velocity = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized * projectileSpeed;

            tempCooldown = cooldown;
        }

        if(tempIFrames > 0)
        {
            tempIFrames -= Time.deltaTime;
        }
        else
        {
            tempIFrames = 0;
        }

        if(tempCooldown > 0)
        {
            tempCooldown -= Time.deltaTime;
        }
        else
        {
            tempCooldown = 0;
        }
    }
}
