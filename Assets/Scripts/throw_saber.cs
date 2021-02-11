



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]


public class throw_saber : MonoBehaviour
{
    public SteamVR_TrackedController forcehand;
    private bool forceactive = false;
    private bool record = false;
    public List<Gesture> forcemovements;
    public float thresholdmovement = 0.05f;
    private List<Vector3> data, datainterest;
    public bool training_mode = false;
    public string forcemotionname;
    private bool hasregognized;

    private Gesture forcegesture;
    private Transform initpossaber;
    public GameObject lightsaber;
    private int counter = 0;
    private GameObject targetfroce;
    private bool lokedtarget = false;
    private Vector3 Offset = new Vector3(0.0f, 1.0f, 0.0f);
    private bool init;

    public Image lifebar;
    public Image manabar;

    private float throwforce = 600;


    // Start is called before the first frame update
    void Start()
    {
        forcehand.TriggerClicked += onTrigclic;
        forcehand.TriggerUnclicked += offTrigclic;
        forcegesture = new Gesture();
        targetfroce = new GameObject();
        initpossaber = lightsaber.transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (forceactive && !record)
        {
            Initmov();
        }


        if (forceactive && record)
        {
            UpdateMov();
        }

        if (!forceactive && record)
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


    public void onTrigclic(object sender, ClickedEventArgs e)
    {
        forceactive = true;
    }

    public void offTrigclic(object sender, ClickedEventArgs e)
    {
        forceactive = false;
    }

    void Initmov()
    {
        data = new List<Vector3>();
        datainterest = new List<Vector3>();
        record = true;
        data.Add(forcehand.transform.position);

    }

    void UpdateMov()
    {
        Vector3 currentpos = forcehand.transform.position;
        Vector3 lastpos = data[data.Count - 1];
        if (Vector3.Distance(currentpos, lastpos) > thresholdmovement)
        {
            data.Add(forcehand.transform.position);
            datainterest.Add(data[data.Count - 1] - data[data.Count - 2]);
        }
    }

    Gesture EndMov()
    {
        Gesture g = new Gesture();
        if (training_mode)
        {
            if (data.Count > 1)
            {

                g.name = forcemotionname;
                g.Postion_data = datainterest;
                forcemovements.Add(g);
            }
        }

        else
        {
            if (forcemovements.Count > 0)
            {
                if (datainterest.Count > 4)
                    g = Recognise_gesture();
                else
                    Debug.Log("not a motion");

            }
            else
            {
                Debug.Log("NO forcemovement saved");
            }


        }
        record = false;
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
            int min = Mathf.Min(9, datainterest.Count);
            //minG = Mathf.Min(min, minG);
            for (int i = 0; i < min; i++)
            {
                sumDist += Vector3.Distance(datainterest[i], mov.Postion_data[i]);
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

        if (force.name == "throw")
        {

            Vector3 direction = forcehand.transform.forward;

            
            if (counter < 100)
            {
                lightsaber.transform.position += direction * 10.0f * Time.smoothDeltaTime;
                lightsaber.transform.Rotate(new Vector3(0,10,0)* 100.0f * Time.smoothDeltaTime);

            }
            counter++;
            if (counter < 200 && counter >99)
            {
                lightsaber.transform.position -= direction * 10.0f * Time.smoothDeltaTime;
                lightsaber.transform.Rotate(new Vector3(0, -10, 0) * 100.0f * Time.smoothDeltaTime);
            }
            if (counter == 200)
            {
                counter = 0;
                hasregognized = false;
                lightsaber.transform.position = forcehand.transform.position;
                lightsaber.transform.rotation = initpossaber.rotation;
                return;
                
            }


        }
        

        

       // if (init)
       //     init = false;



    }




}



