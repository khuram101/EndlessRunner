using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageInteraction : Interaction
{
    [SerializeField] private bool isTrigger = false;
    [SerializeField] private float force = 100;
    [SerializeField] private GameObject popParticle;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = isTrigger;
    }


    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagee damage))
        {
            damage.Damage();
            Instantiate(popParticle, transform.position, Quaternion.identity);
            //ThrowProjectile();
            gameObject.SetActive(false);
        }

    }



    void ThrowProjectile()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(Vector3.back * force / 7 + new Vector3(transform.position.x * force, force, 0));
    }
}
