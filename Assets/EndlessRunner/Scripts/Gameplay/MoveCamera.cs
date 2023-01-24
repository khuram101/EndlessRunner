using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    [SerializeField, Range(0,-50) ]  private float speed = 5;
    [SerializeField] private float startTime = 2;
    private bool isStart = false;
 
    // Update is called once per frame
    void Update()
    {

        if(isStart)
        transform.position += speed * Time.deltaTime * Vector3.forward;
        else
        {
            startTime -= Time.deltaTime;
            if (startTime <= 0)
                isStart = true;
        }
    }
}
