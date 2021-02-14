using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attack_boss : MonoBehaviour
{
    public Image lifebar;
    public GameObject Maxiexplosion;
    public GameObject hit;
    public Vector2 lifemin = new Vector2(-140,0);
    public AudioClip explose;
    public AudioClip hurt;


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
                        this.gameObject.GetComponent<AudioSource>().clip = explose;
                        this.gameObject.GetComponent<AudioSource>().Play();
                        GameObject.Destroy(GameObject.Instantiate(Maxiexplosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 5);
                        GameObject.Destroy(this.gameObject.transform.parent.gameObject);

                    }
                    else
                    {
                        lifebar.rectTransform.offsetMax -= new Vector2(1, 0);
                        GameObject.Destroy(GameObject.Instantiate(hit, other.gameObject.transform.position, other.gameObject.transform.rotation), 5);
                        this.gameObject.GetComponent<AudioSource>().clip = hurt;
                        this.gameObject.GetComponent<AudioSource>().Play();
                    }


                }
            }

            else
            {
                if (lifebar.rectTransform.offsetMax.x < lifemin.x)
                {
                    this.gameObject.GetComponent<AudioSource>().clip = explose;
                    this.gameObject.GetComponent<AudioSource>().Play();
                    GameObject.Destroy(GameObject.Instantiate(Maxiexplosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 5);
                    GameObject.Destroy(this.gameObject.transform.parent.gameObject);
                }
                else
                {
                    lifebar.rectTransform.offsetMax -= new Vector2(1, 0);
                    GameObject.Destroy(GameObject.Instantiate(hit, other.gameObject.transform.position, other.gameObject.transform.rotation), 5);
                    this.gameObject.GetComponent<AudioSource>().clip = hurt;
                    this.gameObject.GetComponent<AudioSource>().Play();
                }
            }
            

            

           
        }
    }
}
