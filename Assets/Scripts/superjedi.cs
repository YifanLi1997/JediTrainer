using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]

public class superjedi : MonoBehaviour
{
    public SteamVR_TrackedController forcehandR;
    public SteamVR_TrackedController forcehandL;

    private bool forceactiveR = false;
    public bool forceactiveL = false;
    private bool forceactive = false;

    public bool record = false;

    public List<Gesture> forcemovements;
    public float thresholdmovement = 0.05f;
    private List<Vector3> dataR, datainterestR;
    private List<Vector3> dataL, datainterestL;
    public bool training_mode = false;
    public string forcemotionname;
    private bool hasregognized;
    private GameObject leftSaber;

    private Gesture forcegesture;
    private Transform initpossaber;
    public GameObject lightsaberR;
    private int counter = 0;
    private GameObject targetfroce;
    private bool lokedtarget = false;
    private Vector3 Offset = new Vector3(0.0f, 1.0f, 0.0f);
    private bool init;

    public Image manabar;
    public GameObject shockwave;
    public GameObject lighteffect;

    private float throwforce = 600;


    // Start is called before the first frame update
    void Start()
    {
        forcehandR.TriggerClicked += onTrigclicR;
        forcehandR.TriggerUnclicked += offTrigclicR;
        forcehandL.TriggerClicked += onTrigclicL;
        forcehandL.TriggerUnclicked += offTrigclicL;

        forcegesture = new Gesture();
        targetfroce = new GameObject();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (forceactiveR && forceactiveL && !record)
        {
            init = true;
            Initmov();
            
        }

        if (forceactiveR && forceactiveL && record)
        {
            UpdateMov();
            
        }

        if (!forceactiveR && !forceactiveL && record)
        {
            forcegesture = EndMov();
            hasregognized = !forcegesture.Equals(new Gesture());

            //Debug.Log(hasregognized);
            //Debug.Log(lokedtarget);
        }

        if (hasregognized)
        {
            //Debug.Log("ok");
            
            Callforce(forcegesture);
        }


    }


    public void onTrigclicR(object sender, ClickedEventArgs e)
    {
        forceactiveR = true;
    }

    public void offTrigclicR(object sender, ClickedEventArgs e)
    {
        forceactiveR = false;
    }
    public void onTrigclicL(object sender, ClickedEventArgs e)
    {
        forceactiveL = true;
    }

    public void offTrigclicL(object sender, ClickedEventArgs e)
    {
        forceactiveL = false;
    }

    void Initmov()
    {
        dataR = new List<Vector3>();
        datainterestR = new List<Vector3>();
        record = true;
        dataR.Add(forcehandR.transform.position);

        dataL = new List<Vector3>();
        datainterestL = new List<Vector3>();
        dataL.Add(forcehandL.transform.position);

    }

    void UpdateMov()
    {
        Vector3 currentpos = forcehandR.transform.position;
        Vector3 lastpos = dataR[dataR.Count - 1];
        if (Vector3.Distance(currentpos, lastpos) > thresholdmovement)
        {
            dataR.Add(forcehandR.transform.position);
            datainterestR.Add(dataR[dataR.Count - 1] - dataR[dataR.Count - 2]);
        }

        Vector3 currentposL = forcehandL.transform.position;
        Vector3 lastposL = dataL[dataL.Count - 1];
        if (Vector3.Distance(currentposL, lastposL) > thresholdmovement)
        {
            dataL.Add(forcehandL.transform.position);
            datainterestL.Add(dataL[dataL.Count - 1] - dataL[dataL.Count - 2]);
        }
    }

    Gesture EndMov()
    {
        Gesture g = new Gesture();
        if (training_mode)
        {
            if (dataR.Count > 1 && dataL.Count > 1)
            {
                g.name = forcemotionname;
                datainterestR.AddRange(datainterestL);
                g.Postion_data = datainterestR;
                forcemovements.Add(g);
            }
        }

        else
        {
            if (forcemovements.Count > 0)
            {
                if (datainterestR.Count > 4 && datainterestL.Count > 4)
                {
                    datainterestR.AddRange(datainterestL);
                    g = Recognise_gesture();
                }
                else
                    Debug.Log("not a motion");

            }
            else
            {
                Debug.Log("NO forcemovement saved");
            }


        }
        record = false;
        dataR = new List<Vector3>();
        datainterestR = new List<Vector3>();

        dataL = new List<Vector3>();
        datainterestL = new List<Vector3>();
        return g;
    }

    Gesture Recognise_gesture()
    {
        Gesture regonied_move = new Gesture();

        float sumDistmin = Mathf.Infinity;
        // float minG = Mathf.Infinity;
        foreach (var mov in forcemovements)
        {
            //Debug.Log(mov.name);
            float sumDist = 0.0f;
            int min = Mathf.Min(9, datainterestR.Count);
            //minG = Mathf.Min(min, minG);
            for (int i = 0; i < min; i++)
            {
                sumDist += Vector3.Distance(datainterestR[i], mov.Postion_data[i]);
            }

            if (sumDist < sumDistmin)
            {
                regonied_move = mov;
                sumDistmin = sumDist;
            }
        }
        return regonied_move;
    }



    void Callforce(Gesture force)
    {

        if (init)
        {
            leftSaber = GameObject.Instantiate(lightsaberR, forcehandL.transform.position, forcehandL.transform.rotation*Quaternion.Euler(0,-90,0)) as GameObject;
            leftSaber.transform.parent = forcehandL.transform;
            GameObject.Destroy(GameObject.Instantiate(shockwave, forcehandL.transform.position, forcehandL.transform.rotation), 5);

            GameObject.Destroy(GameObject.Instantiate(lighteffect, forcehandL.transform.position, forcehandL.transform.rotation), 5);
            GameObject.Destroy(GameObject.Instantiate(lighteffect, forcehandL.transform.position, forcehandL.transform.rotation), 5);
            GameObject.Destroy(GameObject.Instantiate(lighteffect, forcehandL.transform.position, forcehandL.transform.rotation), 5);
            GameObject.Destroy(GameObject.Instantiate(lighteffect, forcehandL.transform.position, forcehandL.transform.rotation), 5);
            GameObject.Destroy(GameObject.Instantiate(lighteffect, forcehandL.transform.position, forcehandL.transform.rotation), 5);

            leftSaber.GetComponent<throw_saber>().forcehand = forcehandL;
            leftSaber.GetComponent<throw_saber>().otherhand = forcehandR;
            manabar.rectTransform.offsetMax -= new Vector2(150, 0);
        }
        counter++;
        if (counter > 1000)
        {
            GameObject.Destroy(leftSaber);
            counter = 0;
            hasregognized = false;
            return;
        }

        




        if (init)
            init = false;




    }




}
