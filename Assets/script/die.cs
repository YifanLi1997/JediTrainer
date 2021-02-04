using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class die : MonoBehaviour
{
    public GameObject Lightsaber;
    public GameObject tinyexplosion;

    //public AudioClip explosion;
    private Vector3 Offset = new Vector3(0.0f, 1.0f, 0.0f);
    ushort intensity = 5000;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("before collided");
        if (other.gameObject == Lightsaber)
        {


            Debug.Log("collided");
            //this.gameObject.GetComponent<AudioSource>().clip = explosion;
            //this.gameObject.GetComponent<AudioSource>().Play();

            GameObject.Instantiate(tinyexplosion, this.transform.position+ Offset, this.transform.rotation);


            //GameObject.Destroy(this.gameObject);
            GameObject.Destroy(this.gameObject.transform.parent.gameObject);
            ViveInput.TriggerHapticPulse(HandRole.RightHand, intensity);
            /*Input XRController = Input.GetDeviceAtXRNode(XRNode.RightHand);
            if (XRController.IsValid)
            {
                HapticCapabilities hapcap = new HapticCapabilities();
                XRController.TryGetHapticCapabilities(out hapcap);

                Debug.Log("DOES XR CONTROLLER SUPPORT HAPTIC BUFFER: " + hapcap.supportsBuffer);
                Debug.Log("DOES XR CONTROLLER SUPPORT HAPTIC IMPULSE: " + hapcap.supportsImpulse);
                Debug.Log("DOES XR CONTROLLER SUPPORT HAPTIC CHANNELS: " + hapcap.numChannels);
                Debug.Log("DOES XR CONTROLLER SUPPORT HAPTIC BUFFER FREQUENCY HZ: " + hapcap.bufferFrequencyHz);

                if (hapcap.supportsImpulse)
                {
                    XRController.SendHapticImpulse(0, 0.5f);
                }
            }*/
            //Input.Channels[1].Preempt(new OVRHapticsClip(noize, 1));
            //GameObject.Destroy(tinyexplosion);
        }
    }

}
