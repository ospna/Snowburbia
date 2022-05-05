using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class RPGMovement : MonoBehaviour
{
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

    private void Awake()
    {
        //intialize 
        CapsuleCollider = GetComponent<CapsuleCollider>();
        RigidBody = GetComponent<Rigidbody>();
        _refObject = Camera.main.transform;
        CurrentSpeed = 0f;
        Acceleration = 4f;
    }

    private void FixedUpdate()
    {
        //do the movement and rotation here
        if (Steer)
        {
            Move(MovementDirection);
        }

        if (Track)
        {
            Rotate(RotationDirection);
        }
    }

    // Resets this instance
    public void Reset()
    {
        Velocity = Vector3.zero;
    }

    // Disables steering
    public void SetSteeringOff()
    {
        Steer = false;
    }

    // Enables steering
    public void SetSteeringOn()
    {
        Steer = true;
    }

    // Disables tracking
    public void SetTrackingOff()
    {
        Track = false;
    }

    // Enables tracking
    public void SetTrackingOn()
    {
        Track = true;
    }

    // Initializes this instance with passed data
    public void Init(float acceleration, float agility, float rotationSpeed, float speed)
    {
        Acceleration = acceleration = 4f;
        RotationSpeed = rotationSpeed;
        Speed = speed = 4f;
    }

    // Moves this instance in the specified direction
    public void SetMoveDirection(Vector3 direction)
    {
        //set the movement direction
        MovementDirection = direction;
    }

    // Moves this instance to the target
    public void SetMoveTarget(Vector3 target)
    {
        //find the direction to target
        Vector3 direction = target - transform.position;
        direction.y = 0f;

        //set the direction
        SetMoveDirection(direction);
    }

    // Moves this instance
    public void Move(Vector3 direction)
    {
        //accelerate
        CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, Speed, Acceleration * Time.deltaTime);

        //move the character in this direction
        RigidBody.MovePosition(transform.position + MovementDirection.normalized * CurrentSpeed * Time.deltaTime);
    }

    // Rotates this instance to face the specified direction
    public void SetRotateFaceDirection(Vector3 direction)
    {
        //set the rotation direction
        RotationDirection = direction;
    }

    // Faces the position
    public void SetRotateFacePosition(Vector3 target)
    {
        //find the rotation direction
        Vector3 direction = target - transform.position;
        direction.y = 0f;

        //set the rotation direction
        SetRotateFaceDirection(direction);
    }

    // Rotates this instance
    public void Rotate(Vector3 direction)
    {
        //rotate if we have direction
        if (direction.magnitude >= 0.01f)
        {
            //get the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            //rotate to target
            Quaternion currTargetRotation;
            currTargetRotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

            //rotate here
            RigidBody.MoveRotation(currTargetRotation);

        }
    }

    // Property to access the velocity of the player
    public Vector3 Velocity
    {
        get
        {
            return RigidBody.velocity;
        }

        set
        {
            RigidBody.velocity = value;
        }
    }
}
