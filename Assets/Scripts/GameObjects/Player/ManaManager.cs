using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour {

    public delegate void SetValue(float val);
    public static event SetValue SetMana;
    public static event SetValue SetMaxMana;

    public delegate void AbsorbMana(Vector2 point, ArrayList affected);
    public static event AbsorbMana PullMana;

    [SerializeField]
    private float mana;
    [SerializeField]
    private int maxMana;
    [SerializeField]
    private float absorbRadius;
    [SerializeField]
    private LayerMask layerMask;

    Collider2D collider;
    Collider2D[] hitColliders;

    ArrayList affected = new ArrayList();

    // Use this for initialization
    void Start ()
    {
        Mana.RequestCollider += GetCollider;

        TempHurt.AddMaxMana += ChangeMaxMana;

        AbilityManager.ChangeMana += ChangeMana;

        collider = GetComponent<Collider2D>();

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
        affected.Clear();

        hitColliders = Physics2D.OverlapCircleAll(transform.position, absorbRadius, layerMask);

        for(int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Mana" && Physics2D.GetIgnoreCollision(collider, hitColliders[i]))
            {
                affected.Add(hitColliders[i].name);
            }
            else
            {
                Debug.Log("Eey, summat's wrong 'ere");
            }
        }

        if(affected.Count != 0)
        {
            PullMana(transform.position, affected);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mana")
        {
            ChangeMana(1);

            Destroy(collision.gameObject);
        }
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
