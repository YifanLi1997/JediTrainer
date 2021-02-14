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
    private float rand1, rand2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        rand1 = Random.Range(25, 75);
        rand2 = Random.Range(75, 125);
        if (counter == (int)rand1)
        {
            //instantiate a bullet
            GameObject laser1 = Instantiate(bullet, offset2.transform.position+ offset, offset2.transform.rotation) as GameObject;

            laser1.transform.LookAt(thePlayer.transform);

            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
        }

        if (counter == (int)rand1 +4)
        {
            //instantiate a bullet
            
            GameObject laser2 = Instantiate(bullet, offset2.transform.position - offset, offset2.transform.rotation) as GameObject;
            laser2.transform.LookAt(thePlayer.transform);

            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
        }

        if (counter == (int)rand2)
        {
            //instantiate a bullet
            GameObject laser3 = Instantiate(bullet, offset1.transform.position + offset, offset1.transform.rotation) as GameObject;

            laser3.transform.LookAt(thePlayer.transform);

            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
        }

        if (counter > rand2 + 4)
        {
            //instantiate a bullet

            GameObject laser4 = Instantiate(bullet, offset1.transform.position - offset, offset1.transform.rotation) as GameObject;
            laser4.transform.LookAt(thePlayer.transform);
            counter = 0;
            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
            //GameObject.Instantiate(bullet, offset2.transform.position, offset2.transform.rotation);
        }
    }
}
