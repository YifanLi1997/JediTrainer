using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_to_traget : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject bullet;
    public float speed = 1.0f;
    public GameObject offset;
    int counter = 0;
    

    void Start()
    {
        //init code
    }

    void FixedUpdate()
    {
        //look at the player
        this.gameObject.transform.LookAt(new Vector3(thePlayer.transform.position.x, this.gameObject.transform.position.y, thePlayer.transform.position.z));

        //move towards the player
        this.gameObject.transform.position += transform.forward *speed * Time.smoothDeltaTime;

        counter++;

        if (counter > 300)
        {
            //instantiate a bullet
            GameObject.Instantiate(bullet, offset.transform.position, offset.transform.rotation);
            counter = 0;
        }
    }
}
