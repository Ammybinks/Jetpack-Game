using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDestroyable {

    public delegate void SetFlag();
    public static event SetFlag EndGame;

    public delegate void SetValue(float val);
    public static event SetValue SetHealth;
    public static event SetValue SetMaxHealth;
    
    public float maxHealth;
    public float health;
    public float iFrames;
    
    private float tempIFrames;

    // Use this for initialization
    void Start()
    {
        if (SetHealth != null)
        {
            SetHealth(health);
        }

        if (SetMaxHealth != null)
        {
            SetMaxHealth(maxHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Max out working health
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeHealth(maxHealth);
        }
        
        ////Maximum Health
        //+10 to maximum health
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeMaxHealth(10);
        }
        //-10 to maximum health
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeMaxHealth(-10);
        }
        
        if(tempIFrames > 0)
        {
            tempIFrames -= Time.deltaTime;
        }
        else if(tempIFrames != 0)
        {
            tempIFrames = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollisions(collision);
    }
    
    void CheckCollisions(Collider2D collision)
    {
        if (tempIFrames == 0)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy tempEnemy = collision.GetComponent<Enemy>();

                ChangeHealth(-tempEnemy.damage);

                tempIFrames = iFrames;
            }
            if(collision.gameObject.tag == "EnemyProjectile")
            {
                ProjectileHit tempProjectile = collision.GetComponent<ProjectileHit>();

                ChangeHealth(-tempProjectile.damage);

                tempIFrames = tempProjectile.iFrames;
            }
        }
    }

    private void ChangeHealth()
    {
        SetHealth(health);
    }
    private void ChangeHealth(float change)
    {
        health += change;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (SetHealth != null)
        {
            SetHealth(health);
        }

        if (health <= 0)
        {
            Kill();
        }
    }

    private void ChangeMaxHealth(int change)
    {
        maxHealth += change;

        if (maxHealth < 0)
        {
            maxHealth = 0;
        }
        if (health > maxHealth)
        {
            health = maxHealth;

            if (SetHealth != null)
            {
                SetHealth(health);
            }
        }

        if (SetMaxHealth != null)
        {
            SetMaxHealth(maxHealth);
        }
    }

    public void Kill()
    {
        if(EndGame != null)
        {
            EndGame();
        }

        Destroy(gameObject);
    }
}
