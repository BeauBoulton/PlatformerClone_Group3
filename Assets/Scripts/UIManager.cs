using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
/*
 * Name: Beau Boulton
 * Date: 4/9/25
 * Description: Pulls health from player and displays it in the UI
 */

public class UIManager : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text healthText;
    public TMP_Text eTanksText;
    public TMP_Text jumpBoostText; 
    public TMP_Text heavyBulletText;

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentPlayerHealth > 0)
        {
            healthText.text = "Energy: " + playerController.currentPlayerHealth;
        }

        if (playerController.currentPlayerHealth <= 0)
        {
            healthText.text = "Energy: 0";
        }

        eTanksText.text = "Energy Tanks: " + playerController.eTanks;

        if (playerController.hasJumpBoost)
        {
            jumpBoostText.text = "Jump Boost"; 
        }

        else
        {
            jumpBoostText.text = " ";
        }

        if (playerController.hasHeavyBullets)
        {
            heavyBulletText.text = "Heavy Bullets"; 
        }

        else
        {
            heavyBulletText.text = " ";
        }
    }
}
