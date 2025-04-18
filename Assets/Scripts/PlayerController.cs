using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;
/*
 * Name: Beau Boulton, Nick Sumek
 * Date: 4/8/25
 * Description: Handles player movement and HP. 
 * Last Edited: 4/15/25
 */

public class PlayerController : MonoBehaviour
{
    // Jump force added when player presses space
    public float jumpForce = 8f;
    // Bool to check if player picked up jump boost
    public bool hasJumpBoost = false;
    // Variable to check if player is currently jumping for double jump
    private int isJumping = 0;
    // Player movement speed
    public float speed = 10f;
    // Bool for heavy bullets
    public bool hasHeavyBullets = false; 
    // Rigidbody to add force to
    private Rigidbody rigidbody;

    // Health and damage variables
    public int maxPlayerHealth = 99; 
    public int currentPlayerHealth = 99;
    public int enemyDamage = 15; 
    public int fallDepth;
    public int gameOverScene; 
    private Vector3 startPosition;

    // Variables for iframes
    private bool isInvincible = false;
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
        if (currentPlayerHealth <= 0)
        {
            SceneManager.LoadScene(gameOverScene);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the object colliding is tagged as Hazard, decrease health
        if (collision.gameObject.tag == "Hazard")
        {
            // Checks if the player is not invincible so that health isn't remuved during iframes
            if (!isInvincible)
            {
                // Gets enemy damage variable from enemy and sets it to the local enemyDamage variable
                enemyDamage = collision.gameObject.GetComponent<EnemyController>().enemyDamage; 
                // Removes health and starts iframes
                currentPlayerHealth -= enemyDamage;
                StartCoroutine(IFrames());
            }
        }

        // If Samus collides with a wall, raycasts to find if the wall is to the left or right and
        // then pushes Samus in the opposite direction
        if (collision.gameObject.tag == "Wall")
        {
            RaycastHit hit;
            // If the raycast returns true then an object has been hit and the player is touching a wall 
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 1f))
            {
                rigidbody.MovePosition(transform.position += Vector3.left * speed * 2 * Time.deltaTime);
            }
            
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 1f))
            {
                rigidbody.MovePosition(transform.position += Vector3.right * speed * 2 * Time.deltaTime);
            }
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the collider is the jump boost, set hasJumpBoost to true to enable double jump
        if (other.gameObject.tag == "Jump Boost")
        {
            hasJumpBoost = true;
            Destroy (other.gameObject);
        }
        
        // If the collider is the health pickup, increase max health by 100 and 
        if (other.gameObject.tag == "Health Pickup")
        {
            maxPlayerHealth += 100; 
            currentPlayerHealth = maxPlayerHealth; 
            Destroy (other.gameObject);
        }

        // If the collider is the health pack, increase health by the amount in the health pack
        if (other.gameObject.tag == "Health Pack")
        {
            // Prevents player from picking up health pack if their health is already full
            if (currentPlayerHealth < maxPlayerHealth)
            {
                // Getting the health variable from the health pack
                int addedHealth = other.GetComponent<HealthPackScript>().addedHealth;
                currentPlayerHealth += addedHealth;

                // Makes sure that current health can't go above max health 
                if (currentPlayerHealth > maxPlayerHealth)
                {
                    currentPlayerHealth = maxPlayerHealth;
                }

                Destroy(other.gameObject);
            }
        }

        // If the collider is the gun upgrade, set has heavy bullets to true
        if (other.gameObject.tag == "upgrade")
        {
            hasHeavyBullets = true;
            Destroy(other.gameObject); 
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
            currentPlayerHealth -= 15;
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
            CheckIfJumping();

            if (!hasJumpBoost)
            {
                // If the player does not have the jump pack, they can only jump when they are touching the floor
                // isJumping is always set to 0 when the player is touching the floor
                if (isJumping == 0)
                {
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }

            if (hasJumpBoost) 
            { 
                // If the player has the jump boost, they can jump if isJumping is 0 or 1 so they can jump when they are on the floor or if they have jumped once
                // Once they have jumped twice they can no longer keep jumping until touching the floor
                if (isJumping <= 1)
                {
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
        }
    }

    /// <summary>
    /// Checks if the player is jumping for the purpose of double jumping
    /// </summary>
    private void CheckIfJumping()
    {
        RaycastHit hit;
        // If the raycast returns true then an object has been hit and the player is touching the floor
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            // Sets isJumping ot 0 if the player is touching the floor
            isJumping = 0;
        }
        
        else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Increases isJumping every time the player presses W or Up and is not touching the floor
                isJumping++; 
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
