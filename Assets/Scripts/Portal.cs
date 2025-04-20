using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Nick Sumek
 * updated 4/ 15 / 25
 * sets up the portal function
 */

public class Portal : MonoBehaviour
{
    public Vector3 teleportPoint;

    public GameObject spawnPoint;

    private void Start()
    {
        teleportPoint = spawnPoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //sets pos of touched obj to the teleport point
        other.transform.position = teleportPoint;

    }
}
