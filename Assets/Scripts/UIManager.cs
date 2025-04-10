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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Energy: " + playerController.playerHealth;
    }
}
