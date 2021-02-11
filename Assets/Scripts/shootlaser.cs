using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootlaser : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.gameObject.transform.position += this.gameObject.transform.forward * 5.0f * Time.smoothDeltaTime;//this.gameObject.transform.parent.gameObject.transform.forward * 1.0f * Time.smoothDeltaTime;
    }
}
