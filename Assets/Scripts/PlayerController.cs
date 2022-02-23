using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public Vector3 direction;

    public float speed = 3;
    public float jumpForce = 10;
    public float gravity = -20;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform model;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        direction.x = horizontalInput * speed;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        direction.y += gravity * Time.deltaTime;

        if(isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
                animator.SetBool("isJumping", true);
            }
            else
            {
                animator.SetBool("isJumping", false);
            }
        }

        if(horizontalInput != 0)
        {
            animator.SetBool("isMoving", true);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, 0));
            model.rotation = newRotation;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        controller.Move(direction * Time.deltaTime);
    }
}
