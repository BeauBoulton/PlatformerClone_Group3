using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 *Nick Sumek
 * updated 4/ 15 / 25
 * sets up the final portal function
 */

public class LastRoomPortal : MonoBehaviour
{
    public int sceneToLoad;//need the index for the final room

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}


