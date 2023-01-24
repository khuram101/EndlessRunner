using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollosionObject : MonoBehaviour, ICollision
{
    [SerializeField] private Platform currentPlatform;
    public void OnCollosionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IDamage>() != null)
            other.gameObject.GetComponent<IDamage>().OnDamage();

    }



    public void OnTriggerExit(Collider other)
    {
       
        if(currentPlatform)
        currentPlatform. ShiftToNextPoint();

    }

    public void OnDamage()
    {
        throw new System.NotImplementedException();
    }

    public void OnDamage(float amount)
    {
        throw new System.NotImplementedException();
    }
}

public interface ICollision
{
    void OnTriggerExit(Collider other);
    void OnCollosionEnter(Collision other);
}
public interface IDamage
{
    void OnDamage();
    void OnDamage(float amount);
}
