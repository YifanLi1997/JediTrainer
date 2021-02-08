using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled = 0f;

    public GameObject facingTarget;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.gameObject.transform.LookAt(facingTarget.transform);

        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
}
