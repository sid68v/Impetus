using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    enum Status
    {
        Alive, Transcending, Dying
    }

    #region PUBLIC VARIABLES
    [SerializeField]
    float thrustFactor = 20.0f;
    [SerializeField]
    float rotationFactor = 1.0f;
    [SerializeField] Status status = Status.Alive;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (status != Status.Alive) { return; }  // if not alive, no collision to be checked.

        switch (collision.gameObject.tag)
        {
            case "Friendly": // do nothing. 
                break;

            case "Finish":
                status = Status.Transcending;
                Invoke("GoToNextLevel", 1f);
                break;
            default:
                status = Status.Dying;
                Invoke("OnDeath", 1f);
                break;
        }

        Debug.Log("Collision" + collision.gameObject.name);
    }

    private void OnDeath()
    {
        // reset to initial level.
        SceneManager.LoadScene(0);
    }

    private void GoToNextLevel()
    {
        // won the level.
        SceneManager.LoadScene(1);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (status == Status.Alive)
        {
            Thrust();
            RotatePlayer();
        }
    }

    private void Thrust()
    {
        // thrust handling
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustFactor * Time.deltaTime, ForceMode.Impulse);
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
