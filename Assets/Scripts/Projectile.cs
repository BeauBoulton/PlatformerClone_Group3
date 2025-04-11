using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Nick Sumek
 * 4/ 8 / 25
 * sets up the projectile prefab
 */

public class Projectile : MonoBehaviour
{
    [Header("Pojectile Variables")]
    public float speed = 10;
    public bool goingLeft;

    public float stunTimer;

    // Update is called once per frame
    void Update()
    {
        if (goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }

        else//going right
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }
        
        

    }

    private void OnTriggerEnter(Collider other)
    {
        print("Hit something");
        print("Tag of hit object is: " + other.tag);
        if(other.gameObject.tag != "Player" )
        {
            print("Hit non-player");
            Destroy(gameObject);
        }
    }


}
