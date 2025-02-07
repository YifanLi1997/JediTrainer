﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMoveToPlayer : MonoBehaviour
{
    [SerializeField] GameObject shootingTarget;

    [SerializeField] float laserSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(shootingTarget.transform);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, UnityEngine.Random.Range(0f, 360f));
    } 

    // Update is called once per frame
    void Update()
    {
        
        transform.position += transform.forward * laserSpeed * Time.smoothDeltaTime;
    }
}
