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
    public bool reflected = false;

    public GameObject tinyexplosion;
    public AudioClip refle;
    public AudioClip hurt;

    //public SteamVR_TrackedObject sabercontrol;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("before collided");
        if (other.tag == "Lightsaber")
        {

            Ray ray = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
            RaycastHit hitInfo;
            bool hasGroundTarget = Physics.Raycast(ray, out hitInfo);
            this.gameObject.transform.forward = Vector3.Reflect(this.gameObject.transform.forward, hitInfo.normal);
            this.gameObject.transform.forward += new Vector3(Random.RandomRange(-3, 3), Random.RandomRange(0, 8), Random.RandomRange(0, 3));
            //this.gameObject.transform.forward *= -1;//  Vector3.Reflect(other.transform.forward, hitInfo.normal);
            //Vector3 normal = new Vector3(-Lightsaber.transform.position.x, -Lightsaber.transform.position.y+ other.transform.position.y,-Lightsaber.transform.position.z + other.transform.position.z);
            // this.gameObject.transform.forward = this.gameObject.transform.forward - 2 * normal * Vector3.Dot(normal, this.gameObject.transform.forward);
            //GameObject.Destroy(GameObject.Instantiate(tinyexplosion, hitInfo.transform.position, Quaternion.identity), 0.1f);
            SteamVR_Controller.Input((int)other.transform.parent.GetComponent<SteamVR_TrackedObject>().index).TriggerHapticPulse((ushort)500);
            other.gameObject.GetComponent<AudioSource>().clip = refle;
            other.gameObject.GetComponent<AudioSource>().Play();
            reflected = true;

}
        if (other.gameObject == player)
        {
            lifebar.rectTransform.offsetMax -= new Vector2(10, 0);
            other.gameObject.GetComponent<AudioSource>().clip = hurt;
            other.gameObject.GetComponent<AudioSource>().Play();
            //play music

            GameObject.Destroy(this.gameObject);



        }
    }
}
