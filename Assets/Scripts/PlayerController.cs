using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Name: Beau Boulton
 * Date: 4/8/25
 * Description: Handles player movement and HP. 
 */

public class PlayerController : MonoBehaviour
{
    // Jump force added when player presses space
    public float jumpForce = 8f;
    // Player movement speed
    public float speed = 10f;
    // Rigidbody to add force to
    private Rigidbody rigidbody;

    // Respawn variables
    public int playerHealth = 99; 
    public int fallDepth;
    private Vector3 startPosition;

    // Variables for iframes
    public bool isInvincible = false; 
    public int iFramesSpeed;
    public float blinkSpeed;
    public MeshRenderer meshRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        // Set reference to player's attached rigidbody
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object colliding is tagged as Hazard, decrease 
        if (other.gameObject.tag == "Hazard")
        {
            if (!isInvincible)
            {
                playerHealth -= 15;
                StartCoroutine(IFrames()); 
            }
        }
    }

    /// <summary>
    /// Moves player left and right using arrow keys or "a" and "d"
    /// </summary>
    private void Move()
    {
        Vector3 add_position = Vector3.zero;

        //Inputs of a and d or arrow keys work
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // If the player inputs the a key or left arrow, move player object left
            rigidbody.MovePosition(transform.position += Vector3.left * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0); 
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // If the player inputs the d key or right, move player object right
            rigidbody.MovePosition(transform.position += Vector3.right * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        transform.position += add_position;
        if (transform.position.y < fallDepth)
        {
            // Respawn player if they fall out of bounds
            Respawn();
        }
    }

    /// <summary>
    /// Allows player to jump by pressing W or up arrow
    /// </summary>
    private void Jump()
    {
        // Handles jumping
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            RaycastHit hit;
            // If the raycast returns true then an object has been hit and the player is touching the floor
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                Debug.Log("Touching the ground");
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            else
            {
                Debug.Log("Cannot jump, not touching the ground");
            }

        }
    }

    /// <summary>
    /// Respawns the player and deducts a life if the player has more than 0 lives
    /// </summary>
    private void Respawn()
    {
        // Respawns player at start position and deducts a life
        transform.position = startPosition;
    }

    private void Blinking()
    {
        
    }

    IEnumerator BlinkTimer()
    {
        yield return new WaitForSeconds(blinkSpeed); 
    }

    IEnumerator IFrames()
    {
        isInvincible = true; 
        yield return new WaitForSeconds(iFramesSpeed);
        isInvincible = false; 
    }
}
