using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 10f;
    [SerializeField] AudioClip mainengine;
    [SerializeField] ParticleSystem rocketBooster;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem rightThruster;

    ParticleSystem anyThruster;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotatingLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotatingRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainengine);
        rocketBooster.Play();
    }

    void StopThrusting()
    {
        audioSource.Stop();
        rocketBooster.Stop();
    }

    void RotatingRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThruster.isPlaying) leftThruster.Play();
    }

    void RotatingLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThruster.isPlaying) rightThruster.Play();
    }

    void StopRotating()
    {
        rightThruster.Stop();
        leftThruster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //fixing the physics
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing the fix
    }
}
