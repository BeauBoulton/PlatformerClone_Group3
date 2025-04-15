using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        //sets pos of touched obj to the teleport point
        other.transform.position = teleportPoint.position;


    }
}
