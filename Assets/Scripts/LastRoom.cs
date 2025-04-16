using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
/*
 * Name: Beau Boulton
 * Date: 4/15/25
 * Description: Handles last door opening when all enemies in last room are dead
 */
public class LastRoom : MonoBehaviour
{
    public Item[] enemyArray;
    
    public int enemiesInRoom;

    private int enemiesLeft; 

    // Start is called before the first frame update
    void Start()
    {
        enemyArray = new Item[enemiesInRoom];
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemiesLeft();
        if (enemiesLeft == 0);
        {
            // do something
        }
    }

    private void CheckForEnemiesLeft()
    {
        enemiesLeft = enemiesInRoom;
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (enemyArray[i] == null)
            {
                enemiesLeft--; 
            }
        }
    }
}
