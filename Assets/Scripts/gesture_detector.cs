using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> Postion_data;
    //public UnityEvent Onregognize;

}

public class gesture_detector : MonoBehaviour
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
    private GameObject target;
    public string ennemi;
    private int counter = 0;
    private GameObject targetfroce;
    private bool lokedtarget = false;
    private Vector3 Offset = new Vector3(0.0f, 1.0f, 0.0f);
    private bool init;

    public GameObject forceeffect;
    public GameObject explose;
    public GameObject healeffect;

    public Image lifebar;
    public Image manabar;


    // Start is called before the first frame update
    void Start()
    {
        forcehand.TriggerClicked += onTrigclic;
        forcehand.TriggerUnclicked += offTrigclic;
        forcegesture = new Gesture();
        targetfroce = new GameObject();

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

        if (hasregognized && (lokedtarget || forcegesture.name == "heal"))
        {
            //Debug.Log("ok");

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
            if (target.name == ennemi)
            {
                lokedtarget = true;
                init = true;
                targetfroce = target.gameObject.transform.parent.gameObject;
            }
        }
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
                if(datainterest.Count>4)
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



    void Callforce(string name)
    {
        Debug.Log(name);
        if (targetfroce == null && name != "heal")
        {
            lokedtarget = false;
            return;
        }

        if (name == "pull")
        {

            targetfroce.transform.LookAt(forcehand.transform);
            targetfroce.transform.position += targetfroce.transform.forward * 5 * Time.smoothDeltaTime;
            //targetfroce.transform.Rotate(new Vector3(Random.Range(0.0f, 90.0f), Random.Range(0.0f, 90.0f), Random.Range(0.0f, 90.0f)));
            if (init)
            {
                GameObject.Instantiate(forceeffect, targetfroce.transform.position + Offset, targetfroce.transform.rotation);
                manabar.rectTransform.offsetMax -= new Vector2(30, 0);
            }
        }

        if (name == "push")
        {
            targetfroce.transform.LookAt(forcehand.transform);
            targetfroce.transform.position -= targetfroce.transform.forward * 5 * Time.smoothDeltaTime;
            targetfroce.transform.position += targetfroce.transform.up * 1 * Time.smoothDeltaTime;

            if (init)
            {
                GameObject.Instantiate(forceeffect, targetfroce.transform.position + Offset, targetfroce.transform.rotation);
                manabar.rectTransform.offsetMax -= new Vector2(30, 0);
            }
            counter++;
            if (counter > 100)
            {
                GameObject.Destroy(GameObject.Instantiate(explose, targetfroce.transform.position + Offset, targetfroce.transform.rotation), 3);
                GameObject.Destroy(targetfroce);
                lokedtarget = false;
                counter = 0;
            }

        }
        if (name == "lightning")
        {

            if (init)
            {
                forcehand.transform.GetChild(1).gameObject.SetActive(true);
            }
            manabar.rectTransform.offsetMax -= new Vector2(1, 0);

            forcehand.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.localPosition
            = new Vector3(0.0f, (targetfroce.transform.position - forcehand.transform.position).y, (targetfroce.transform.position - forcehand.transform.position).z);
            SteamVR_Controller.Input((int)forcehand.controllerIndex).TriggerHapticPulse(5000);

            counter++;
            if (counter > 100)
            {
                GameObject.Destroy(GameObject.Instantiate(explose, targetfroce.transform.position + Offset, targetfroce.transform.rotation), 3);
                GameObject.Destroy(targetfroce);
                forcehand.transform.GetChild(1).gameObject.SetActive(false);
                lokedtarget = false;
                counter = 0;
            }

        }

        if (name == "heal")
        {

            lifebar.rectTransform.offsetMax += new Vector2(1, 0);
            manabar.rectTransform.offsetMax -= new Vector2(2, 0);
            GameObject.Destroy(GameObject.Instantiate(healeffect, forcehand.transform.position, forcehand.transform.rotation), 3);
            counter++;
            if (counter > 100)
            {
                hasregognized = false;
                counter = 0;
                name = null;
            }

        }



        if (init)
            init = false;



    }




}