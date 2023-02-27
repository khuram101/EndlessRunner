using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    
    public virtual void OnTriggerEnter(Collider other)
    {

    }

    public virtual void OnTriggerExit(Collider other)
    {

    }


    public virtual void OnTriggerStay(Collider other)
    {

    }


    public virtual void OnCollisionEnter(Collision collision)
    {

    }

    public virtual void OnCollisionExit(Collision collision)
    { }


}
public interface IDamagee
{
    void Damage(float value);
    void Damage();
}
public interface IDirection
{
    void SetDirection();

}