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
    public int iFramesTime = 5;
    public float blinkSpeed;
    public MeshRenderer body;
    public MeshRenderer head;
    public MeshRenderer visor;
    public MeshRenderer rShoulder;
    public MeshRenderer lShoulder;
    public MeshRenderer gun; 

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

    private void OnCollisionEnter(Collision collision)
    {
        // If the object colliding is tagged as Hazard, decrease health
        if (collision.gameObject.tag == "Hazard")
        {
            // Checks if the player is not invincible so that health isn't remuved during iframes
            if (!isInvincible)
            {
                // Removes health and starts iframes
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
            // Damages and respawns player if they fall out of bounds
            playerHealth -= 15;
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
    /// Respawns the player at startPosition
    /// </summary>
    private void Respawn()
    {
        // Respawns player at start position and puts them in iframes
        transform.position = startPosition;
        StartCoroutine(IFrames()); 
    }

    /// <summary>
    /// Starts the BlinkDelay coroutine
    /// </summary>
    public void Blink()
    {
        StartCoroutine(BlinkDelay());
    }

    // This is a coroutine, it disables the mesh renderers for the player, starts a timer for the duration of the blink speed variable, then re-enables the mesh renderers
    IEnumerator BlinkDelay()
    { 
        body.enabled = false;
        head.enabled = false;
        visor.enabled = false;
        rShoulder.enabled = false;
        lShoulder.enabled = false;
        gun.enabled = false;
        yield return new WaitForSeconds(blinkSpeed);
        body.enabled = true;
        head.enabled = true;
        visor.enabled = true;
        rShoulder.enabled = true;
        lShoulder.enabled = true;
        gun.enabled = true;
    }

    // This is a coroutine, it is a timer. It makes the player invincible and invokes Blink repeating for iFramesTime number of seconds
    IEnumerator IFrames()
    {
        // Set isInvincible to true so player can't take damage while coroutine is running
        isInvincible = true;
        // Invokes Blink repeatedly at a rate of the blinkSpeed variable * 2
        InvokeRepeating("Blink", 0, (blinkSpeed * 2)); 
        // Sets a timer for iFramesTime number of seconds
        yield return new WaitForSeconds(iFramesTime);
        // Removes invincibility
        isInvincible = false;
        // Stops invoking Blink
        CancelInvoke("Blink"); 
        // Enables all mesh renderers in case they were disabled at the end of the timer 
        body.enabled = true;
        head.enabled = true;
        visor.enabled = true;
        rShoulder.enabled = true;
        lShoulder.enabled = true;
        gun.enabled = true;
    }
}
