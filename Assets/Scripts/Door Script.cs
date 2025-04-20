using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Nick Sumek
 * updated 4/ 15 / 25
 * sets up the doors to be destroyed when shot
 */

public class DoorScript : MonoBehaviour
{
    //makes the door dissapear after being shot by either a normal or heavy shot
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Bullet2")
        {
            Destroy(GetComponentInParent<DoorScript>().gameObject);
            Destroy(gameObject);
        }
    }
}
