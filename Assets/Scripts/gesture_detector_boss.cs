using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]


public class gesture_detector_boss : MonoBehaviour
{
    public SteamVR_TrackedController forcehand;
    public SteamVR_TrackedController otherhand;

    private bool forceactive = false;
    private bool otheractive = false;
    private bool record = false;
    private bool init;
    private bool doingsomething = false;

    public List<Gesture> forcemovements;
    public float thresholdmovement = 0.05f;
    private List<Vector3> data, datainterest;
    public bool training_mode = false;
    public string forcemotionname;
    private bool hasregognized;

    private Gesture forcegesture;
    private GameObject target;
    //public string ennemi;
    private int counter = 0;
    private GameObject targetfroce;
    private bool lokedtarget = false;
    private Vector3 Offset = new Vector3(0.0f, 1.0f, 0.0f);
    private int number_of_child;

    public GameObject healeffect;

    public Image lifebar;
    public Image manabar;


    // Start is called before the first frame update
    void Start()
    {
        forcehand.TriggerClicked += onTrigclic;
        forcehand.TriggerUnclicked += offTrigclic;
        otherhand.TriggerClicked += onTrigclicO;
        otherhand.TriggerUnclicked += offTrigclicO;
        forcegesture = new Gesture();
        targetfroce = new GameObject();
        number_of_child = forcehand.gameObject.transform.childCount + 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (forceactive && !record && !doingsomething && !otheractive)
        {
            Initmov();
        }


        if (forceactive && record && !doingsomething && !otheractive)
        {
            UpdateMov();
        }

        if (!forceactive && record && !doingsomething)
        {
            forcegesture = EndMov();
            hasregognized = !forcegesture.Equals(new Gesture());
            Debug.Log(forcegesture.name);
            Debug.Log(hasregognized);
            if (forcehand.gameObject.transform.childCount > number_of_child)
                hasregognized = false;
            if (otheractive)
                hasregognized = false;
            //Debug.Log(hasregognized);
            //Debug.Log(lokedtarget);
        }

        if (hasregognized && (lokedtarget || forcegesture.name == "heal") )
        {
            Callforce(forcegesture.name);
        }


    }


    public void onTrigclic(object sender, ClickedEventArgs e)
    {
        forceactive = true;

        Ray ray = new Ray(forcehand.transform.position, forcehand.transform.forward);
        RaycastHit hitInfo;
        bool hasGroundTarget = Physics.Raycast(ray, out hitInfo);
        if (hasGroundTarget)
        {
            target = hitInfo.collider.gameObject;
            //Debug.Log(target.name);
            if (target.name == "Shield")
            {
                lokedtarget = true;
                init = true;
                targetfroce = target.gameObject;
            }
        }
    }

    public void offTrigclic(object sender, ClickedEventArgs e)
    {
        forceactive = false;
    }

    public void onTrigclicO(object sender, ClickedEventArgs e)
    {
        otheractive = true;
    }

    public void offTrigclicO(object sender, ClickedEventArgs e)
    {
        otheractive = false;
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
            int min = Mathf.Min(18, datainterest.Count);
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



    void Callforce(string name)
    {
        
        if (targetfroce == null && name != "heal")
        {
            lokedtarget = false;
            return;
        }

        
        if (name == "lightning")
        {
            doingsomething = true;
            if (init)
            {
                forcehand.transform.GetChild(1).gameObject.SetActive(true);
            }
            manabar.rectTransform.offsetMax -= new Vector2(1, 0);

            forcehand.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.localPosition
            = forcehand.transform.forward * Vector3.Distance(forcehand.transform.position, targetfroce.transform.position);//new Vector3(0.0f, (targetfroce.transform.position - forcehand.transform.position).y + 2, (targetfroce.transform.position - forcehand.transform.position).z);
            SteamVR_Controller.Input((int)forcehand.controllerIndex).TriggerHapticPulse(5000);

            if(!target.gameObject.GetComponent<boss_shield>().shieldoff)
            {
                target.gameObject.GetComponent<boss_shield>().hitTime = 500;
                target.gameObject.GetComponent<boss_shield>().mat.SetFloat("_HitTime", 500);
            }

            else
            {
                targetfroce = this.gameObject.transform.parent.transform.GetChild(0).gameObject;
                if (targetfroce.GetComponent<attack_boss>().lifebar.rectTransform.offsetMax.x < -140)
                {
                    GameObject.Destroy(GameObject.Instantiate(targetfroce.GetComponent<attack_boss>().Maxiexplosion, targetfroce.transform.position, targetfroce.transform.rotation), 5);
                    GameObject.Destroy(targetfroce.transform.parent.gameObject);
                    forcehand.transform.GetChild(1).gameObject.SetActive(false);
                    lokedtarget = false;
                    counter = 0;
                    doingsomething = false;
                    hasregognized = false;
                    return;
                }
                else
                {
                    
                    targetfroce.GetComponent<attack_boss>().lifebar.rectTransform.offsetMax -= new Vector2(0.5f, 0);
                }
            }


            counter++;
            if (counter > 100)
            {
                if (target.name == "Shield")
                    target.GetComponent<boss_shield>().hitcount += 4;
                forcehand.transform.GetChild(1).gameObject.SetActive(false);
                lokedtarget = false;
                counter = 0;
                doingsomething = false;
                hasregognized = false;
            }

        }

        if (name == "heal")
        {
            doingsomething = true;
            lifebar.rectTransform.offsetMax += new Vector2(1, 0);
            manabar.rectTransform.offsetMax -= new Vector2(2, 0);
            GameObject.Destroy(GameObject.Instantiate(healeffect, forcehand.transform.position, forcehand.transform.rotation), 3);
            counter++;
            if (counter > 100)
            {
                hasregognized = false;
                counter = 0;
                name = null;
                doingsomething = false;
            }

        }



        if (init)
            init = false;



    }




}