using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableCoin : Interaction
{
    [SerializeField] private bool isTrigger = false;


    private void Start()
    {
        GetComponent<Collider>().isTrigger = isTrigger;
    }


    public override void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

}
