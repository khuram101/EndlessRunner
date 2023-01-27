using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] protected float startTime = 2;
    protected bool isStart = false;
    public virtual void Update() { }
    public virtual void StartMovement() { }
}
