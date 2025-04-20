using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Nick Sumek, Beau Boulton
 * 4/ 8 / 25
 * sets up the gun arm as a spawn location for pre fab projectiles
 *  * Last edited: 4/15/25
 */

public class GunArmScript : MonoBehaviour
{
    //projectile variables
    public bool goingLeft;
    private bool canFire = true;
    private float playerRotation;
    //spawner variables

    public float shotCoolDown = .5f;
 
    // PlayerController to get the hasHeavyBullet variable
    public PlayerController playerController;

    // Game objects for normal bullet, heavy bullet, and which bullet to use
    public GameObject normalBullet;
    public GameObject heavyBullet;
    public GameObject bulletToUse; 


    public float timeBetweenShots;
    public float startDelay;

    private void Update()
    {
        playerRotation = Mathf.RoundToInt(transform.parent.rotation.eulerAngles.y);//updates with char position

        shotDirection();//checks for character rotaion

        //sets the space bar action to be what shoots
        if (canFire && Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
            StartCoroutine(ShotDelay());
        }
    }

    //sets up the spawning of projectiles
    public void SpawnProjectile()
    {
        // Gets hasHeavyBullets from PlayerController
        bool hasHeavyBullets = playerController.hasHeavyBullets;
        
        // Bullet to use is normal bullet by default
        bulletToUse = normalBullet;

        // If hasHeavyBullets form PlayerController is true, bullet to use is heavy bullet
        if (hasHeavyBullets == true)
        {
           bulletToUse = heavyBullet;
        }

        // Instantiates projectile using the bulletToUse object at the position of the gun arm
        GameObject projectile = Instantiate(bulletToUse, transform.position, bulletToUse.transform.rotation);
        if (projectile.GetComponent<Projectile>())
        {   
            projectile.GetComponent<Projectile>().goingLeft = goingLeft;
        }
    }

    IEnumerator ShotDelay()
    {
        canFire = false;
        yield return new WaitForSeconds(shotCoolDown);
        canFire = true;
    }

    //decides which way to shoot the projectile
    private void shotDirection()
    {
        if (playerRotation == 0)
        {
            goingLeft = false;
        }

        if (playerRotation == 180)
        {
            goingLeft = true;
        }
    }


}
