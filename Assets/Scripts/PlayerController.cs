﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    #region PUBLIC VARIABLES
    public float thrustFactor = 20.0f;
    public float rotationFactor = 1.0f;
    #endregion

    #region PRIVATE_VARIABLES
    Rigidbody rb;
    AudioSource audioSource;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        RotatePlayer();
    }

    private void Thrust()
    {
        // thrust handling
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustFactor*Time.deltaTime,ForceMode.Impulse);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void RotatePlayer()
    {
        rb.freezeRotation = true;

        // rotation handling
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward * rotationFactor * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(Vector3.back * rotationFactor * Time.deltaTime);
        }

        rb.freezeRotation = false;
    }


}