using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour, IDestroyable
{
    [SerializeField]
    protected int abilityIndex;
    [SerializeField]
    protected Rigidbody2D projectile;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float recoil;
    [SerializeField]
    protected float cooldown;
    [SerializeField]
    protected Rigidbody2D mana;
    [SerializeField]
    protected float manaCost;
    [SerializeField]
    protected float manaSpeed;
    [SerializeField]
    protected float variance;
    [SerializeField]
    protected float spread;
    
    protected Vector2 bulletVelocity;
    protected Vector2 recoilVelocity;
    protected Vector2 manaVelocity;

    protected Rigidbody2D projectileInstance;
    protected Rigidbody2D manaInstance;

    protected float timer;

    protected float rotation;

    protected float degrees;

    protected int manaIndex;

    protected bool castable = true;

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                timer = 0;
            }
        }
    }

    protected virtual void Awake()
    {
        AbilityManager.SetIndex += CheckIndex;
    }

    protected void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            timer = 0;
        }
    }

    public virtual void CheckIndex(int val)
    {
        if (val == abilityIndex - 1)
        {
            AbilityManager.BeginFiring += BeginFiring;
            AbilityManager.Fire += Fire;
            AbilityManager.StopFiring += StopFiring;
            ManaManager.SetMana += CheckMana;
        }
        else
        {
            AbilityManager.BeginFiring -= BeginFiring;
            AbilityManager.Fire -= Fire;
            AbilityManager.StopFiring += StopFiring;
            ManaManager.SetMana -= CheckMana;
        }
    }

    protected virtual void CheckMana(float val)
    {
        if (val >= manaCost)
        {
            castable = true;
        }
        else
        {
            castable = false;
        }
    }

    protected virtual float BeginFiring(float radians)
    {
        return 0;
    }

    protected virtual float Fire(float radians)
    {
        return 0;
    }

    protected virtual void StopFiring()
    {

    }

    public void test(string word)
    {

    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
