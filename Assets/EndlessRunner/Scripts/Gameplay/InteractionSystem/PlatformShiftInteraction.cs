using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShiftInteraction : Interaction
{
    [SerializeField] private bool isTrigger = false;
    [SerializeField] private Platform currentPlatform;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = isTrigger;
    }


    public override void OnCollisionEnter(Collision collision)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnCollisionExit(Collision collision)
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagee damage) && currentPlatform)
        {
            currentPlatform.ShiftToNextPoint();
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        //throw new System.NotImplementedException();
    }
}
