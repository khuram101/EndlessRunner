using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGizmo : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float radius = 1;


    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
