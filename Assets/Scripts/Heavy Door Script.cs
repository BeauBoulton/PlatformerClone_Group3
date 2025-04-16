using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Nick Sumek
 * updated 4/ 15 / 25
 * sets up the doors to be destroyed only when hit with a heavy bullet
 */

public class HeavyDoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet2")//needs heavy bullet to pass through the door
            Destroy(gameObject);

    }

}
