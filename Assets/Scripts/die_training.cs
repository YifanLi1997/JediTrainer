using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class die_training : MonoBehaviour
{
    public GameObject Lightsaber;
    public GameObject tinyexplosion;
    public SteamVR_TrackedObject sabercontrol;

    //public AudioClip explosion;
    private Vector3 Offset = new Vector3(0.0f, 1.0f, 0.0f);
    ushort intensity = 50000;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("before collided");
        if (other.gameObject.tag == "Lightsaber")
        {

            GameObject.Destroy(GameObject.Instantiate(tinyexplosion, this.transform.position, this.transform.rotation), 3);


            //GameObject.Destroy(this.gameObject);
            GameObject.Destroy(this.gameObject.transform.parent.gameObject.transform.parent.gameObject);
            //ViveInput.TriggerHapticPulse(HandRole.RightHand, intensity);
            SteamVR_Controller.Input((int)sabercontrol.index).TriggerHapticPulse(intensity);

        }
    }

}
