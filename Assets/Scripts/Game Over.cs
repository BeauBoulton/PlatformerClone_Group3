using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


/*
 *Nick Sumek
 * updated 4/ 15 / 25
 * the quit game function and the setting up calling different scenes from the build manager
 */


public class GameOver : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();

    }

    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

