using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Nick Sumek, Beau Boulton
 * 4/ 8 / 25
 * sets up the projectile prefab
 * last edited 4/20/25
 */

public class Projectile : MonoBehaviour
{
    [Header("Pojectile Variables")]
    public float speed = 12;
    public bool goingLeft;

    private Vector3 spawnLocation;

    // Start is called before the first frame update
    private void Start()
    {
        // On spawn sets its spawn location so that it can destreoy itself if it moves too far from this position
        spawnLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }

        else //going right
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }

        // If projectile moves more than 20 units from its spawn location it destroys itself
        // in case a projectile ever gets out of bounds and never hits a wall or enemy
        if (transform.position.x > spawnLocation.x + 20 || transform.position.x < spawnLocation.x - 20)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the projectile collides with something other than the player and the enemy counter object in the last room it destroys itself
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy Counter" )
        {
            Destroy(gameObject);
        }
    }
}
