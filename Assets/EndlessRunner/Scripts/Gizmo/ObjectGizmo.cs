using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGizmo : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float radius = 1;
    [SerializeField] private bool isRectangle = false;


    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = color;
        if(!isRectangle)
        Gizmos.DrawSphere(transform.position, radius);
        else
        {
            BoxCollider col = GetComponent<BoxCollider>();
            Gizmos.DrawCube(transform.position, new Vector3(col.size.x, col.size.y, col.size.z));
        }
    }
}
