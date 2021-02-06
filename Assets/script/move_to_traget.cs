using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_to_traget : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject bullet;
    public float speed = 1.0f;

    int counter = 0;
    

    void Start()
    {
        //init code
    }

    void FixedUpdate()
    {
        //look at the player
        this.gameObject.transform.LookAt(thePlayer.transform);

        //move towards the player
        this.gameObject.transform.position += transform.forward *speed * Time.smoothDeltaTime;

        counter++;

        if (counter > 100)
        {
            //instantiate a bullet
            //GameObject.Instantiate(bullet, this.transform.position+ bullet.transform.position, this.transform.rotation* bullet.transform.rotation);
            counter = 0;
        }
    }
}
