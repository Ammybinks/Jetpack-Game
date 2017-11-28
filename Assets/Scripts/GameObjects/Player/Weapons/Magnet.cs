using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : WeaponBase
{
    public delegate void AbsorbMana(Vector2 point);
    public static event AbsorbMana PullMana;

    protected override void CheckIndex(int val)
    {
        if (val == abilityIndex - 1)
        {
            AbilityManager.BeginFiring += Fire;
            AbilityManager.Fire += Fire;
            ManaManager.SetMana += CheckMana;
        }
        else
        {
            AbilityManager.BeginFiring -= Fire;
            AbilityManager.Fire -= Fire;
            ManaManager.SetMana -= CheckMana;
        }
    }

    protected override float Fire(float radians)
    {
        if(PullMana != null)
        {
            PullMana(transform.position);
        }

        return -1;
    }
}
