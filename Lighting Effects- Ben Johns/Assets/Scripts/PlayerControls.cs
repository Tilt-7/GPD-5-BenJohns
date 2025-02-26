using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Variables

    // Access Unity APIs for components
    public CharacterController controller;
    public Animator anim;

    // Assign an audio clip file and access the AudioSource API
    public AudioClip runningSound;
    private AudioSource audioSource;

    // Check in Inspector if GameObject is Player One or Player Two
    public bool playerOne = true;
    public bool playerTwo = false;

    // Values for rotation, jump height, and running speeds
    public float runningSpeed = 4.0f;
    public float rotationSpeed = 100.0f;
    public float jumpHeight = 6.0f;

    // Declare player input variables
    private float jumpInput;
    private float runInput;
    private float rotateInput;

    // Declare a 3D vector for moving
    public Vector3 moveDir;

    // Starting Function
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Assign player input from Project Settings to variables
        if (playerOne)
        {
            runInput = Input.GetAxis("Vertical");
            rotateInput = Input.GetAxis("Horizontal");
            if (controller.isGrounded)
            {
                jumpInput = Input.GetAxis("Jump");
            }
        } else {
            runInput = Input.GetAxis("VerticalTwo");
            rotateInput = Input.GetAxis("HorizontalTwo");
            if (controller.isGrounded)
            {
                jumpInput = Input.GetAxis("JumpTwo");
            }
        }

        // Set moveDir to new Vector3 based on player input
        moveDir = new Vector3(0, jumpInput * jumpHeight, runInput * runningSpeed);
        // Update the character's direction based on the game world and player input
        moveDir = transform.TransformDirection(moveDir);
        // Move the character using the controller in the direction and new position set earlier
        controller.Move(moveDir * Time.deltaTime);

        // Update character rotation
        transform.Rotate(0f, rotateInput * rotationSpeed * Time.deltaTime, 0f);

        // Update animations and sound effects based on player input values
        Effects();
    }

    void Effects()
    {
        if (runInput != 0 && jumpInput == 0)
        {
            anim.SetBool("Run Forward", true);
            if (audioSource != null && !audioSource.isPlaying && controller.isGrounded)
            {
                audioSource.clip = runningSound;
                audioSource.Play();
            }
        } else {
            anim.SetBool("Run Forward", false);
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        if (jumpInput != 0)
        {
            anim.SetBool("Jump", true);
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        } else {
            anim.SetBool("Jump", false);
        }
    }
}