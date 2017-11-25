using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour {

    public delegate void SetValue(float val);
    public static event SetValue SetMana;
    public static event SetValue SetMaxMana;
    
    [SerializeField]
    private float mana;
    [SerializeField]
    private int maxMana;
    [SerializeField]
    private float absorbRadius;
    
    // Use this for initialization
    void Start ()
    {
        AbilityManager.CheckMana += ChangeMana;

        AbilityManager.ChangeMana += ChangeMana;
        Mana.ChangeMana += ChangeMana;
        
        if (SetMana != null)
        {
            SetMana(mana);
        }
        if (SetMaxMana != null)
        {
            SetMaxMana(maxMana);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collided!");

    //    if (collision.gameObject.tag == "Mana")
    //    {
    //        ChangeMana(1);

    //        Destroy(collision.gameObject);
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mana")
        {
            Mana tempMana = collision.GetComponent<Mana>();

            if(tempMana.Timer <= 0)
            {
                ChangeMana(1);

                tempMana.Kill();
            }
        }
    }

    private void ChangeMana()
    {
        SetMana(mana);
    }
    private void ChangeMana(float change)
    {
        mana += change;

        if (mana <= 0)
        {
            mana = 0;
        }
        if (mana > maxMana)
        {
            mana = maxMana;
        }

        if (SetMana != null)
        {
            SetMana(mana);
        }
    }

    private void ChangeMaxMana(int change)
    {
        maxMana += change;

        if (maxMana < 0)
        {
            maxMana = 0;
        }
        if (mana > maxMana)
        {
            mana = maxMana;

            if (SetMana != null)
            {
                SetMana(mana);
            }
        }

        if(SetMaxMana != null)
        {
            SetMaxMana(maxMana);
        }
    }

    private BoxCollider2D[] GetCollider()
    {
        return GetComponents<BoxCollider2D>();
    }
}
