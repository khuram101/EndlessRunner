using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : Movement
{
    [SerializeField, Range(0, -50)] protected float speed = 5;
    private float minSpeed;

    private bool isStopMovement = false;


    private void Start()
    {
        minSpeed = speed;
    }
    public override void Update()
    {
        if (isStart)
            StartMovement();
        else
        {
            startTime -= Time.deltaTime;
            if (startTime <= 0)
                isStart = true;
        }

    }

    public override void StartMovement()
    {
        if (!isStopMovement)
            transform.position += speed * Time.deltaTime * Vector3.forward;
    }

    public void SetSpeed(float val)
    {
        speed = minSpeed + (-val);
    }

    public void Movement(bool IsStatus)
    {
        isStopMovement = IsStatus;
    }
}
