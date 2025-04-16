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
    public int sceneToLoad;//neesd the index for the final room

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        SwitchScene(sceneToLoad);
    }

    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

