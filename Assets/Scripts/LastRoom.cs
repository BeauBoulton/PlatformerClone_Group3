using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;
/*
 * Name: Beau Boulton
 * Date: 4/15/25
 * Description: Handles game ending when all enemies in the last room are dead
 * Last Edited: 4/16/25
 */
public class LastRoom : MonoBehaviour
{
    // Array to place enemies into to count them
    public EnemyController[] enemyArray;
    
    // Public variable to set array size. MUST BE set to the actual number of enemies in the last room
    public int enemiesInRoom;

    // Variable for how many enemies are left in room
    private int enemiesLeft;

    public int winScene; 

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the enemy array
        enemyArray = new EnemyController[enemiesInRoom];
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemiesLeft();
    }

    /// <summary>
    /// Checks which enemies are left in the array lowers the number of enemies left whenever a null slot is detected
    /// if no enemies are left in the array, proceeds to the win screen
    /// </summary>
    private void CheckForEnemiesLeft()
    {
        // Sets the number of enemies left to the number of slots in the array
        enemiesLeft = enemiesInRoom;

        // Checks each array slot and decreases number of enemies left when a null slot is detected
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (enemyArray[i] == null)
            {
                enemiesLeft--; 
            }
        }
        
        // Loads the win screen if there are no enemies left
        if (enemiesLeft == 0)
        {
            SceneManager.LoadScene(winScene);
        }
    }

    /// <summary>
    /// Cycles through array and adds enemies to each null slot
    /// </summary>
    /// <param name="enemyToAdd"></param>
    /// <returns>Returns bool success indicating that it was successful at filling all array slots</returns>
    private bool AddEnemy(EnemyController enemyToAdd)
    {
        bool success = false;

        for  (int i = 0; i < enemyArray.Length; i++)
        {
            // If the index is null, assign the current enemyToAdd to it
            if (enemyArray[i] == null)
            {
                enemyArray[i] = enemyToAdd; 
                success = true;
                break; 
            }
        }

        return success; 
    }

    // OnTriggerEnter to detect collision with all enemies in room
    private void OnTriggerEnter(Collider other)
    {
        // If the collider is tagged as Hazard, assigns the collider to the newEnemy variable and adds the newEnemy to the array
        if (other.gameObject.tag == "Hazard")
        {
            EnemyController newEnemy = other.GetComponent<EnemyController>();
            AddEnemy(newEnemy);
        }
    }
}
