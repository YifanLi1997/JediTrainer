using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_shield : MonoBehaviour
{
    float hitTime;
    Material mat;
    public GameObject lightsaber;
    public int hitcount = 0;
    public int comp = 0;
    public bool shieldoff = false;
    //public GameObject Laserbul;

    void Start()
    {
        if (GetComponent<Renderer>())
        {
            mat = GetComponent<Renderer>().sharedMaterial;
            mat.SetFloat("_Opacity", 0.3f);
        }

    }

    void FixedUpdate()
    {

        if (hitTime > 0)
        {
            float myTime = Time.fixedDeltaTime * 1000;
            hitTime -= myTime;
            if (hitTime < 0)
            {
                hitTime = 0;
            }
            mat.SetFloat("_HitTime", hitTime);
        }

        if (hitcount > 9)
        {
            mat.SetFloat("_Opacity", 0);
            comp++;
            shieldoff = true;
        }
        if(comp>1000)
        {
            comp = 0;
            hitcount = 0;
            this.gameObject.SetActive(true);
            mat.SetFloat("_Opacity", 0.3f);
            shieldoff = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (hitcount < 10)
        {
            if (other.tag == "Lightsaber")
            {
                //mat.SetVector("_HitPosition", transform.InverseTransformPoint(other.contacts[i2].point));
                hitTime = 500;
                mat.SetFloat("_HitTime", hitTime);
                hitcount++;
            }
            if (other.tag == "Laser")
            {
                if (other.gameObject.GetComponent<reflec_laser>().reflected)
                {
                    //mat.SetVector("_HitPosition", transform.InverseTransformPoint(other.contacts[i2].point));
                    hitTime = 500;
                    mat.SetFloat("_HitTime", hitTime);
                    hitcount++;
                }
            }
        }

        

    }
}
