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
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) audioSource.PlayOneShot(mainengine);
            rocketBooster.Play();
        }
        else
        {
            audioSource.Stop();
            rocketBooster.Stop();
        }
    }

    void ProcessRotation ()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightThruster.isPlaying) rightThruster.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);

            if (!leftThruster.isPlaying) leftThruster.Play();
        }
        else
        {
            rightThruster.Stop();
            leftThruster.Stop();
        }
    }

        void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //fixing the physics
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing the fix
    }
}
