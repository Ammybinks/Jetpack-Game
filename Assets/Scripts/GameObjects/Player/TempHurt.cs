using UnityEngine;
using System.Collections;

public class TempHurt : MonoBehaviour {

    public delegate void ChangeHealth(int val);
    public static event ChangeHealth SetHealth;
    public static event ChangeHealth SetMaxHealth;

    public static event AbilityManager.ChangeInt AddMaxMana;

    [SerializeField]
    private int maxHealth;

    private int tempMaxHealth = -1;

    [SerializeField]
    private int health;

    private int tempHealth = -1;
	// Use this for initialization
	void Start ()
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
	void Update ()
    {
        ////Working Health
        //+10 to working health
        if (Input.GetKeyDown(KeyCode.E))
        {
            tempHealth = health + 10;

            if (tempHealth > maxHealth)
            {
                tempHealth = maxHealth;
            }
        }
        //-10 to working health
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tempHealth = health - 10;

            if(tempHealth < 0)
            {
                tempHealth = 0;
            }
        }

        //Max out working health
        if (Input.GetKeyDown(KeyCode.R))
        {
            tempHealth = maxHealth;
        }
        //Min out working health
        if (Input.GetKeyDown(KeyCode.F))
        {
            tempHealth = 0;
        }

        //Set working health
        if(tempHealth != -1)
        {
            health = tempHealth;
            
            if (SetHealth != null)
            {
                SetHealth(health);
            }

            tempHealth = -1;
        }

        ////Maximum Health
        //+10 to maximum health
        if (Input.GetKeyDown(KeyCode.T))
        {
            tempMaxHealth = maxHealth + 10;

            AddMaxMana(10);
        }
        //-10 to maximum health
        if (Input.GetKeyDown(KeyCode.G))
        {
            tempMaxHealth = maxHealth - 10;

            AddMaxMana(-10);

            if (tempMaxHealth < 1)
            {
                tempMaxHealth = 1;
            }
        }

        //Set maximum health
        if (tempMaxHealth != -1)
        {
            maxHealth = tempMaxHealth;

            if(health > maxHealth)
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

            tempMaxHealth = -1;
        }
    }
}
