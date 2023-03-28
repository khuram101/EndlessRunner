using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : Interaction, IDamagee
{
    [SerializeField] private int damageCounter = 0;
    [SerializeField] private UnityEvent OnDamage;
    public void Damage(float value)
    {
        Debug.Log("Damaged " + value);
    }

    public void Damage()
    {
        Debug.Log("Damaged");
        damageCounter++;
        OnDamage?.Invoke();
    }
}
