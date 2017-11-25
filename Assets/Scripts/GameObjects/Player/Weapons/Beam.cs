using UnityEngine;
using System.Collections;

public class Beam : WeaponBase
{
    [SerializeField]
    float turnSpeed;
    [SerializeField]
    float turnSpeedReduction;
    [SerializeField]
    float decay;
    [SerializeField]
    LayerMask hitMask;

    float tempRecoil;

    float localRadians;

    float diff;

    float manaDirection;

    RaycastHit2D hit;
    RaycastHit2D hit2;

    Vector3 tempScale;
    
    protected override float BeginFiring(float radians)
    {
        if (castable && timer == 0)
        {
            //Create an instance of the projectile.
            projectileInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

            projectileInstance.transform.localScale = new Vector3(0, 1);

            projectileInstance.transform.parent = transform.parent;

            tempRecoil = recoil;

            PhysicsCalc(radians);

            Raycast();

            localRadians = radians;

            return manaCost;
        }

        return 0;
    }

    protected override float Fire(float radians)
    {
        if (castable && projectileInstance != null)
        {
            diff = ((radians * Mathf.Rad2Deg)) - ((localRadians * Mathf.Rad2Deg));

            if (diff > 180)
            {
                diff *= -1;
            }
            else if (diff < -180)
            {
                diff *= -1;
            }

            if (localRadians * Mathf.Rad2Deg > 180)
            {
                float temp = localRadians * Mathf.Rad2Deg - 180;
                localRadians = -180 * Mathf.Deg2Rad;
                localRadians -= temp * Mathf.Deg2Rad;

            }
            else if (localRadians * Mathf.Rad2Deg < -180)
            {
                float temp = localRadians * Mathf.Rad2Deg + 180;
                localRadians = 180 * Mathf.Deg2Rad;
                localRadians -= temp * Mathf.Deg2Rad;
            }

            diff *= turnSpeedReduction;

            if (diff > turnSpeed)
            {
                diff = turnSpeed;
            }
            else if (diff < -turnSpeed)
            {
                diff = -turnSpeed;
            }

            localRadians += diff * Mathf.Deg2Rad;

            PhysicsCalc(localRadians);

            Raycast();

            return manaCost;
        }

        return 0;
    }

    protected override void StopFiring()
    {
        if(projectileInstance)
        {
            Destroy(projectileInstance.gameObject);
        }

        timer = cooldown;
    }

    private void PhysicsCalc(float radians)
    {
        //Create the velocity of the projectil
        recoilVelocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (tempRecoil * -1);

        //Set the rotation of the projectile
        Vector3 temp = projectileInstance.gameObject.transform.rotation.eulerAngles;

        rotation = radians * Mathf.Rad2Deg;

        temp.z = rotation;

        projectileInstance.gameObject.transform.rotation = Quaternion.Euler(temp);
        projectileInstance.gameObject.transform.position = transform.position;

        //Accelerate(recoilVelocity);
        MovementManager.Accelerate(recoilVelocity);

        if(decay != -1)
        {
            //Decay recoil velocity
            tempRecoil *= decay;
        }
    }

    void Raycast()
    {
        //Raycast 1
        hit = Physics2D.Raycast(projectileInstance.transform.position, projectileInstance.transform.right, Mathf.Infinity, hitMask);
        Debug.DrawRay(projectileInstance.transform.position, projectileInstance.transform.right, Color.red);
        if (hit)
        {
            //Set scale of projectile
            tempScale = projectileInstance.transform.localScale;

            tempScale.x = hit.distance * 100;
            
            projectileInstance.transform.localScale = tempScale;

            //Expel mana away from collision
            for (int i = 0; i < manaCost; i++)
            {
                manaVelocity = new Vector2(Mathf.Cos(manaDirection), Mathf.Sin(manaDirection)) * (manaSpeed + Random.Range(0, -variance));

                manaInstance = Instantiate(mana, hit.point, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

                manaInstance.name = "Beam Mana " + manaIndex++;

                manaInstance.velocity = hit.normal.Rotate(Random.Range(-spread, spread)) * (manaSpeed + Random.Range(0, -variance));

                if(mana.gameObject.GetComponent<SpriteRenderer>() && projectile.gameObject.GetComponent<SpriteRenderer>())
                {
                    manaInstance.gameObject.GetComponent<SpriteRenderer>().color = projectile.gameObject.GetComponent<SpriteRenderer>().color;
                }
            }
        }
    }
}
