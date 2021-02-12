using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    public GameObject Path;
    public GameObject robot;
    public GameObject gesture_regognjizer;

    private GameObject robot2;

    private GameObject robot3;

    private GameObject robot4;


    private int counteur;

    // Start is called before the first frame update
    void Start()
    {
        robot2 = GameObject.Instantiate(robot, robot.transform) as GameObject;
        robot2.transform.SetParent(this.gameObject.transform);// = this.gameObject;
        robot2.SetActive(false);

        robot3 = GameObject.Instantiate(robot, robot.transform) as GameObject;
        robot3.transform.SetParent(this.gameObject.transform);
        robot3.SetActive(false);


        robot4 = GameObject.Instantiate(robot, robot.transform) as GameObject;
        robot4.transform.SetParent(this.gameObject.transform);
        robot4.SetActive(false);

        counteur = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counteur++;
        if(counteur==1000)
            robot2.SetActive(true);
        if (counteur == 2000)
            robot3.SetActive(true);
        if (counteur == 3000)
            robot4.SetActive(true);
        if ((counteur > 3500) && (this.gameObject.transform.childCount==2))
            this.gameObject.SetActive(false);

    }
}
