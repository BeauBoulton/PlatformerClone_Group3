using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Nick Sumek
 * 4/ 8 / 25
 * controlls gun function
 */


public class NewBehaviourScript : MonoBehaviour
{
    //variables
    public float speed = 10;
    public bool goingLeft;


    // Start is called before the first frame update
    void Update()
    {
        //sets what position the laster fires
        if (goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }

        else//going right
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }

    }

   





}
