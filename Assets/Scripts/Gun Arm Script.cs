using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Nick Sumek
 * 4/ 8 / 25
 * sets up the gun arm as a spawn location for pre fab projectiles
 */

public class GunArmScript : MonoBehaviour
{
    //projectile variables
    public bool goingLeft;
    private bool canFire = true;
    //spawner variables

    public float shotCoolDown = .5f;

    public GameObject projectilePrefab;
    public float timeBetweenShots;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        //need to change to a key stroke "space bar" not invoke repeating
       // InvokeRepeating("SpawnProjectile", startDelay, timeBetweenShots);

        


    }

    private void Update()
    {
        if (canFire && Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
            StartCoroutine(ShotDelay());
        }
    }

    //sets up the spawning of projectiles
    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        if (projectile.GetComponent<Projectile>())
        {
            projectile.GetComponent<Projectile>().goingLeft = goingLeft;

        }

    }
    //sets the space bar action to be what shoots
    
   

    IEnumerator ShotDelay()
    {



        canFire = false;
        yield return new WaitForSeconds(shotCoolDown);
        canFire = true;





    }




}
