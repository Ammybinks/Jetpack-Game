using UnityEngine;
using System.Collections;

public class Grapple : WeaponBase
{
    bool created;
    bool destroyed;

    bool released;
    
    protected override float BeginFiring(float radians)
    {
        released = true;
        
        return Fire(radians);
    }

    protected override float Fire(float radians)
    {
        if (castable && released)
        {
            if (projectileInstance != null)
            {
                if(projectileInstance.velocity == new Vector2(0, 0) && released)
                {
                    MovementManager.BreakTether();

                    Destroy(projectileInstance.gameObject);

                    released = false;
                }
            }
            else
            {
                //Create an instance of the projectile.
                projectileInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

                recoilVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (recoil * -1);

                rotation = radians * Mathf.Rad2Deg;

                Vector3 temp = projectileInstance.gameObject.transform.rotation.eulerAngles;

                temp.z = rotation;

                projectileInstance.gameObject.transform.rotation = Quaternion.Euler(temp);
                
                //Create the velocity of the projectile.
                bulletVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * speed;

                //Set the projectile's velocity and direction.
                projectileInstance.velocity = bulletVelocity;

                released = false;

                return manaCost;
            }
        }

        released = false;

        return -1;
    }

    //protected override void StopFiring()
    //{
    //    if(!released)
    //    {
    //        if(bulletInstance == null)
    //        {
    //            released = true;
    //        }
    //        else if(bulletInstance.velocity == new Vector2(0,0))
    //        {
    //            released = true;
    //        }
    //    }

    //    if(destroyed)
    //    {
    //        destroyed = false;
    //    }

    //    if(created)
    //    {
    //        created = false;
    //    }
    //}
}
