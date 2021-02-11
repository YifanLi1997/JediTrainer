using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_shooting : MonoBehaviour
{

    public GameObject thePlayer;
    public GameObject bullet;
    public GameObject offset1;
    public GameObject offset2;
    private Vector3 offset = new Vector3(0.05f, 0, 0);
    int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;

        if (counter == 50)
        {
            //instantiate a bullet
            GameObject laser1 = Instantiate(bullet, offset2.transform.position+ offset, offset2.transform.rotation) as GameObject;
            GameObject laser2 = Instantiate(bullet, offset2.transform.position- offset, offset2.transform.rotation) as GameObject;
            laser1.transform.LookAt(thePlayer.transform);
            laser2.transform.LookAt(thePlayer.transform);
            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
        } 
        if (counter > 100)
        {
            //instantiate a bullet
            GameObject laser3 = Instantiate(bullet, offset1.transform.position+offset, offset1.transform.rotation) as GameObject;
            GameObject laser4 = Instantiate(bullet, offset1.transform.position- offset, offset1.transform.rotation) as GameObject;
            laser3.transform.LookAt(thePlayer.transform);
            laser4.transform.LookAt(thePlayer.transform);
            //GameObject.Instantiate(bullet, offset1.transform.position, offset1.transform.rotation);
            //GameObject.Instantiate(bullet, offset1.transform.position, offset1.transform.rotation);
            counter = 0;
        }
    }
}
