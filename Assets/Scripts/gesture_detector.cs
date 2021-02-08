using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> Postion_data;
    
    public UnityEvent Onregognize;

}

public class gesture_detector : MonoBehaviour
{
    public SteamVR_TrackedController forcehand;
    private bool forceactive = false;
    public bool record = false;
    public List<Gesture> forcemovements;

    // Start is called before the first frame update
    void Start()
    {
        forcehand.TriggerClicked += onTrigclic;
        forcehand.TriggerUnclicked += offTrigclic;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(forceactive && record)
        {
            Save();
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

    void Save()
    {
        Gesture g = new Gesture();
        g.name = "Pull";
        List<Vector3> data = new List<Vector3>();
        int compteur = 0;
        while (forceactive && compteur<50)
        {
            data.Add(forcehand.transform.position);
            compteur += 1;
        }

        g.Postion_data = data;
        forcemovements.Add(g);

    }

    //Gesture Recognize()
    //{
    //    Gesture currentgest = new Gesture();
    //    float currentmin = Mathf.Infinity;
    //    foreach (var mov in forcemovements)
    //    {
    //        float sumDist = 0.0f;
    //        bool isDiscarded = false;
    //        for(int i =0; i< mov.Postion_data.Count; i++)
    //        {
    //            Vector3 currentpos = forcehand.transform.position;
    //            float distance = Vector3.Distance(currentpos, mov.Postion_data[i]);

    //        }
    //    }



    //}
  


}
