using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrainingLaser : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform shootingPlace;
    [SerializeField] float shootingTimeGapInSeconds = 2f;
    float shootingTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingTimer >= shootingTimeGapInSeconds)
        {
            Shoot();
            shootingTimer = 0f;
        }
        else
        {
            shootingTimer += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab, shootingPlace.position, shootingPlace.rotation) as GameObject;
    }
}
