using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
public class UserController : MonoBehaviour
{
    
    [SerializeField]
    private float maximumSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Transform cameraTransform;

    private Animator animator;
    private AudioSource audioSrc;

    private float ySpeed;
    private float originalStepOffset;
    private float jumpButtonGracePeriod;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isGrounded;
    private bool isJumping;
    private bool isMoving;

    //public Transform groundCheck;
   // public LayerMask groundLayer;
    public float inputMagnitude;
    //public Vector3 movementDirection;

    [Header("Attributes")]
    public float Acceleration = 1f;
    public float Agility = 1f;
    public float RotationSpeed = 3f;
    public float Speed = 4f;

    public bool Steer;

    public bool Track;

    public float CurrentSpeed { get; set; }

    public CapsuleCollider CapsuleCollider { get; set; }

    public Rigidbody RigidBody { get; set; }

    public Vector3 MovementDirection { get; set; }

    public Vector3 RotationDirection { get; set; }

    Vector3 RefObjectForward;             // The current forward direction of the camera
    Transform _refObject;                 // A reference to the main camera in the scenes transform

    // Start is called before the first frame update
    void Start()
    {
        /*
        //intialize 
        CapsuleCollider = GetComponent<CapsuleCollider>();
        RigidBody = GetComponent<Rigidbody>();
        _refObject = Camera.main.transform;
        CurrentSpeed = 0f;
        /

        // set the ref object
        _refObject = Camera.main.transform;


        //animator = GetComponent<Animator>();
        //characterController = GetComponent<CharacterController>();
        //originalStepOffset = characterController.stepOffset;

        //audioSrc = GetComponent<AudioSource>();
        //GameObject BallGround = GameObject.FindGameObjectWithTag("BallGround");
        //Physics.IgnoreCollision(BallGround.GetComponent<MeshCollider>(), GetComponent<CharacterController>());
    }

    // Update is called once per frame
    void Update()
    {
        //capture input
        float horizontalRot = Input.GetAxis("Horizontal");
        float verticalRot = Input.GetAxis("Vertical");

        //calculate the direction to rotate to
        Vector3 input = new Vector3(horizontalRot, 0f, verticalRot);

        // calculate camera relative direction to move:
        RefObjectForward = Vector3.Scale(_refObject.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 Movement = input.z * RefObjectForward + input.x * _refObject.right;



        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        // slow down
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude *= 2;
        }

        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
        float speed = inputMagnitude * maximumSpeed;
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }


        if (Time.time - lastGroundedTime </= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("isJumping", false);
            isJumping = false;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;

            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            /*  Save for falling animation later
            if ((isJumping && ySpeed <0) || ySpeed < -2)
            {
                animator.SetBool("isFalling", true);
            }
            /


            Vector3 velocity = movementDirection * speed;

            transform.GetComponent<Rigidbody>().MovePosition(velocity * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                animator.SetBool("isMoving", true);
                isMoving = true;
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                if (isMoving)
                {
                    //if (!audioSrc.isPlaying)
                    // audioSrc.Play();
                }
            }
            else
            {
                animator.SetBool("isMoving", false);
                isMoving = false;
                /* if (!isMoving)
                 {
                     audioSrc.Stop();
                 }
                 /
            }
        }




     //capture input
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //calculate the direction to rotate to
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

            // calculate camera relative direction to move:
            movementDirection = Quaternion.AngleAxis(vCam.transform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
            movementDirection.Normalize();

            float speed = inputMagnitude * maximumSpeed;

            Vector3 velocity = movementDirection * speed;

            Owner.RPGMovement.Move(velocity * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                Owner.transform.rotation = Quaternion.RotateTowards(Owner.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }


    }
*/
