using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class futur_seeing : MonoBehaviour
{
    public GameObject realone;
    private float inittrans;
    private int compteur;
    public bool init;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat.SetFloat("_Opacity", 0.8f);
        mat.SetColor("_Color", Color.green);
        mat.SetColor("_ShieldPatternColor", Color.green);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(init)
        {
            this.gameObject.transform.position = realone.transform.position;
            this.gameObject.transform.rotation = realone.transform.rotation;
            inittrans = realone.GetComponent<FollowPath>().distanceTravelled;
            this.gameObject.GetComponent<FollowPath>().speed = 0.7f;
            this.gameObject.GetComponent<FollowPath>().distanceTravelled = inittrans;
            realone.SetActive(false);
            init = false;
            compteur = 0;
        }
        compteur++;
        if (compteur > 500)
        {
            realone.SetActive(true);
            realone.GetComponent<FollowPath>().distanceTravelled = inittrans;
            this.gameObject.SetActive(false);
        }
    }
}
