using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private AudioSource audioSrc;

    public Vector3 direction;

    public float speed = 3;
    public float jumpForce = 10;
    public float gravity = -20;
    //public float inputMagnitude;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform model;

    private bool isGrounded;
    private bool isJumping;
    private bool isThrowing;
    private bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        direction.x = horizontalInput * speed;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        direction.y += gravity * Time.deltaTime;

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
                animator.SetBool("isJumping", true);
                isJumping = true;

                animator.SetBool("isGrounded", false);
                isGrounded = false;
            }
            else
            {
                animator.SetBool("isJumping", false);
                isJumping = false;

                animator.SetBool("isGrounded", true);
                isGrounded = true;
            }
        }

        if (Input.GetButton("Fire1") || Input.GetAxis("Fire1") != 0)
        {
            animator.SetBool("isThrowing", true);
            isThrowing = true;
        }
        else
        {
            animator.SetBool("isThrowing", false);
            isThrowing = false;
        }

        controller.Move(direction * Time.deltaTime);

        if (horizontalInput != 0)
        {
            animator.SetBool("isMoving", true);
            isMoving = true;
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, 0));
            model.rotation = newRotation;

            if (isMoving)
            {
                if (!audioSrc.isPlaying)
                    audioSrc.Play();
            }
        }
        else
        {
            animator.SetBool("isMoving", false);

            isMoving = false;
            if (!isMoving)
            {
                audioSrc.Stop();
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
