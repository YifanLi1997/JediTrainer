using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject robot;
    public GameObject gesture_regognjizer;
    public int number_of_robots = 20;
    public Transform starting_points1, starting_points2;
    private List<GameObject> robotClone;
    private GameObject clone; 



    private int counteur,k,rand;

    // Start is called before the first frame update
    void Start()
    {
        robotClone = new List<GameObject>();
        clone = new GameObject();
        Transform startpos;
        for (int i=0;i<number_of_robots;i++)
        {
            rand = Random.Range(0, 3);
            startpos = (rand < 1 ? robot.transform : (rand < 2 ? starting_points1: starting_points2));
            clone = GameObject.Instantiate(robot, startpos) as GameObject;
            clone.transform.SetParent(this.gameObject.transform);
            robotClone.Add(clone.gameObject);
            robotClone[i].SetActive(false);

        }

        counteur = 0;
        k = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counteur++;
        if (counteur > 300 && k< number_of_robots)
        {
            robotClone[k].SetActive(true);
            k++;
            counteur = 0;
        }
        if ((counteur > 1000) && (this.gameObject.transform.childCount == 3))
            this.gameObject.SetActive(false);

    }
}
