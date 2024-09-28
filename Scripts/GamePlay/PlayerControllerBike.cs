using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBike : MonoBehaviour
{
    // Input variables
    private float horizontalInput;
    private float verticalInput;

    // Player movement and rotation speeds
    public float playerSpeed = 10f;
    public float playerRotationSpeed = 100f;

    // Rigidbody component
    private Rigidbody playerRb;

    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the Rigidbody component
        playerRb = GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting input from the keyboard
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Rotating the player
        transform.Rotate(Vector3.up * Time.deltaTime * horizontalInput * playerRotationSpeed);

        // Moving the player
        Vector3 moveDirection = transform.forward * verticalInput; // Move forward/backward
        playerRb.AddForce(moveDirection * playerSpeed * Time.deltaTime, ForceMode.Impulse);

        if (verticalInput != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();

            }
        }
    }
}
