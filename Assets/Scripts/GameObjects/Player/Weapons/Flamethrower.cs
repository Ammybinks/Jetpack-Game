using UnityEngine;
using System.Collections;

public class Flamethrower : WeaponBase
{
    public delegate void AcceleratePlayer(Vector2 Velocity);
    public static event AcceleratePlayer Accelerate;
    
    protected override float Fire(float radians)
    {
        if (castable)
        {
            //Create an instance of the projectile.
            projectileInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

            //Create the velocity of the projectile.
            bulletVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * speed;
            recoilVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (recoil * -1);

            //Set the projectile's velocity and direction.
            projectileInstance.velocity = bulletVelocity;

            degrees = radians * Mathf.Rad2Deg;

            Vector3 temp = projectileInstance.gameObject.transform.rotation.eulerAngles;

            temp.z = degrees;

            projectileInstance.gameObject.transform.rotation = Quaternion.Euler(temp);
            
            MovementManager.Accelerate(recoilVelocity);

            for (int i = 0; i < manaCost; i++)
            {
                radians = ((degrees + Random.Range(-spread, spread))) * Mathf.Deg2Rad;

                manaVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (manaSpeed + Random.Range(0, -variance));

                manaInstance = Instantiate(mana, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

                manaInstance.name = "Flamethrower Mana " + manaIndex++;

                manaInstance.velocity = manaVelocity;

                if (mana.gameObject.GetComponent<SpriteRenderer>() && projectile.gameObject.GetComponent<SpriteRenderer>())
                {
                    manaInstance.gameObject.GetComponent<SpriteRenderer>().color = projectile.gameObject.GetComponent<SpriteRenderer>().color;
                }
            }

            return manaCost;
        }

        return 0;
    }
}
