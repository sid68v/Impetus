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
    [SerializeField] float thrustFactor = 20.0f;
    [SerializeField] float rotationFactor = 1.0f;

    [SerializeField] Status status = Status.Alive;

    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    [SerializeField] bool isHeadlightsOn = false;
    [SerializeField] bool isImmortal = false;
    [SerializeField] GameObject vehicleLights;
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

        vehicleLights.SetActive(isHeadlightsOn);
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (status != Status.Alive) { return; }  // if not alive, no collision to be checked.

        switch (collision.gameObject.tag)
        {
            case "Friendly": // do nothing. 
                break;
            case "Finish":
                OnFinish();
                break;
            default:if (isImmortal) return;
                OnDeath();
                break;
        }
    }

    private void OnFinish()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();

        status = Status.Transcending;
        Invoke("GoToNextLevel", 1f);
    }

    private void OnDeath()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathParticles.Play();

        status = Status.Dying;
        Invoke("ResetToInitialLevel", 1f);
    }

    private void ResetToInitialLevel()
    {
        // reset to initial level.
        SceneManager.LoadScene(0);
    }

    private void GoToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
        // won the level.
        if (nextSceneIndex != SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("You won the game !");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (status == Status.Alive)
        {
            HandleThrustInputs();
            HandleRotateInputs();
        }
    }

    private void HandleThrustInputs()
    {
        // thrust handling
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            thrustParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rb.AddRelativeForce(Vector3.up * thrustFactor * Time.deltaTime, ForceMode.Impulse);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
            thrustParticles.Play();
        }
    }

    private void HandleRotateInputs()
    {
        rb.angularVelocity = Vector3.zero;
        // rotation handling
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward * rotationFactor * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(Vector3.back * rotationFactor * Time.deltaTime);
        }
    }


}
