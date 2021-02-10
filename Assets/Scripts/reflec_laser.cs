using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public class reflec_laser : MonoBehaviour
{
    public GameObject Lightsaber;
    public GameObject player;
    public Image lifebar;

    public GameObject tinyexplosion;
    public SteamVR_TrackedObject sabercontrol;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("before collided");
        if (other.gameObject == Lightsaber)
        {

            Debug.Log(other.transform.position);
            Vector3 normal = new Vector3(other.transform.position.x, -Lightsaber.transform.position.y+ other.transform.position.y,-Lightsaber.transform.position.z + other.transform.position.z);
            this.gameObject.transform.forward = this.gameObject.transform.forward - 2 * normal * Vector3.Dot(normal, this.gameObject.transform.forward);
            GameObject.Destroy(GameObject.Instantiate(tinyexplosion, other.transform.position, Quaternion.identity), 0.1f);
            SteamVR_Controller.Input((int)sabercontrol.index).TriggerHapticPulse((ushort)500);
            
        }
        if (other.gameObject == player)
        {
            lifebar.rectTransform.offsetMax -= new Vector2(10, 0);

            //play music

            GameObject.Destroy(this.gameObject);



        }
    }
}
