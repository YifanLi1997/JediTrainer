using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attack_boss : MonoBehaviour
{
    public Image lifebar;
    public GameObject Maxiexplosion;
    private Vector2 lifemin = new Vector2(-160,0);


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("before collided");
        if (this.gameObject.transform.parent.GetChild(1).GetComponent<boss_shield>().shieldoff)
        {
            if (other.tag == "Laser")
            {
                if (!other.gameObject.GetComponent<reflec_laser>().reflected)
                    return;
                else
                {
                    if (lifebar.rectTransform.offsetMax.x < lifemin.x)
                    {
                        GameObject.Destroy(GameObject.Instantiate(Maxiexplosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 5);
                        GameObject.Destroy(this.gameObject.transform.parent.gameObject);
                    }
                    else
                    {
                        lifebar.rectTransform.offsetMax -= new Vector2(10, 0);
                    }


                }
            }

            else
            {
                if (lifebar.rectTransform.offsetMax.x < lifemin.x)
                {
                    GameObject.Destroy(GameObject.Instantiate(Maxiexplosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 5);
                    GameObject.Destroy(this.gameObject.transform.parent.gameObject);
                }
                else
                {
                    lifebar.rectTransform.offsetMax -= new Vector2(10, 0);
                }
            }
            

            

           
        }
    }
}
