using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth; 

    // Variables for left and right boundary game objects
    public GameObject leftPoint;
    public GameObject rightPoint;

    // Variables to set boundaries for the enemy based on the game object locations
    private Vector3 leftPos;
    private Vector3 rightPos;

    //Variables for speed and direction
    public int speed;
    public bool goingLeft;

    // Rigidbody to add force to
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        EnemyDie(); 
    }
    
    /// <summary>
    ///  Controls enemy movement back and forth
    /// </summary>
    private void Move()
    {
        // Check if enemy is going left or right
        if (goingLeft)
        {
            RaycastHit hit;
            // If the raycast returns true then an object has been hit and the enemy changes
            // the left boundary to the current position
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.5f))
            {
                leftPos = transform.position;
            }
            // If left boundary has been reached, change direction to right, else continue going left
            if (transform.position.x <= leftPos.x)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
        
        else
        {
            RaycastHit hit;
            // If the raycast returns true then an object has been hit and the enemy changes
            // the right boundary to the current position
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 0.5f))
            {
                rightPos = transform.position;
            }

            // If right boundary is reached, change direction to left, else continue going right
            if (transform.position.x >= rightPos.x)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemyHealth -= 1; 
        }
    }

    private void EnemyDie()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
