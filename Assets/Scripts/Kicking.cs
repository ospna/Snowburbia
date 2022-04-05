using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicking : MonoBehaviour
{
	[Header("Pass and Shot Power Information")]
    public float passspeed = 500;
    public float shootspeed = 1000;
	public float curveShotPowerUp = 400;
	public float shootSpeedForward = 250;
	public float shootSpeedDown = 250;
	public float chipSpeedUp = 1000;
	public float chipSpeedForward = 500;
	public float curveMin;
	public float curveMax;
	public float chipTorqueUp = 500;
	public float dribbleSpeed = 100;
    private float forceMagnitude;

    [Header("Game Objects")]
    private GameObject player;
    private Animator animator;

    [Header("Shot Key Code Info")]
    public KeyCode passKeyCode = KeyCode.Mouse1;
	public KeyCode curveShotKeyCode = KeyCode.Z;
	public KeyCode shootKeyCode = KeyCode.Mouse0;
    public KeyCode chipShotKeyCode = KeyCode.C;

    [Header("References")]
	public GameObject ball;
	public Rigidbody rb;
    public GameObject holdBall;

	[Header("Audio")]
    public AudioSource kickingSound;
    public AudioSource chipSound;
    private AudioSource bounceSound;
    private AudioSource dribbleSound;

    [Header("Bool")]
	public bool addCurve = false;
	public bool addDip = false;
    public bool playerHasBall = false;

    [Header("Animation Bools")]
    private bool isPassing;
    private bool isShooting;
    private bool isChipping;

    // Use this for initialization
    void Start ()
    {
		rb = ball.GetComponent<Rigidbody>();
		player = this.gameObject;
        animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        holdBall.GetComponent<SphereCollider>().enabled = true;
    }

    void OnTriggerStay(Collider other)
    {

        // Pass
		if (Input.GetKeyDown(passKeyCode) && other.gameObject.tag == "SoccerBall")
        {
			rb.AddForce(player.transform.forward * passspeed * Time.deltaTime, ForceMode.Impulse);
            //kickingSound.Play();
            addDip = true;
            holdBall.GetComponent<SphereCollider>().enabled = false;

            animator.SetBool("isPassing", true);
            isPassing = true;
        }

        // Shot
        if (Input.GetKeyDown(shootKeyCode) && other.gameObject.tag == "SoccerBall")
        {
            rb.AddForce(-player.transform.up * shootSpeedDown * Time.deltaTime, ForceMode.Impulse);
            rb.AddForce(player.transform.forward * shootSpeedForward * Time.deltaTime, ForceMode.Impulse);
            //kickingSound.Play();
            addDip = true;
            holdBall.GetComponent<SphereCollider>().enabled = false;

            animator.SetBool("isShooting", true);
            isShooting = true;
        }

        // Curved Shot
        if (Input.GetKeyDown(curveShotKeyCode) && other.gameObject.tag == "SoccerBall")
        {
			rb.AddForce(player.transform.forward * shootspeed * Time.deltaTime, ForceMode.Impulse);
			rb.AddForce(player.transform.up * curveShotPowerUp * Time.deltaTime, ForceMode.Impulse);
            //kickingSound.Play();
            addDip = true;
			addCurve = true;
            holdBall.GetComponent<SphereCollider>().enabled = false;

            animator.SetBool("isShooting", true);
            isShooting = true;
        }

        // Chip Shot
		if (Input.GetKeyDown(chipShotKeyCode) && other.gameObject.tag == "SoccerBall")
        {
			rb.AddForce(player.transform.up * chipSpeedUp * Time.deltaTime, ForceMode.Impulse);
			rb.AddForce(player.transform.forward * chipSpeedForward * Time.deltaTime, ForceMode.Impulse);
			rb.AddTorque(-player.transform.right * chipTorqueUp * Time.deltaTime, ForceMode.Impulse);
            //chipSound.Play();
            addDip = true;
            holdBall.GetComponent<SphereCollider>().enabled = false;

            animator.SetBool("isChipping", true);
            isChipping = true;
        }
	}

	void Update()
    {
		if (addDip == true)
        {
			StartCoroutine (DipAdd ());
		}

		if (addCurve == true)
        {
			StartCoroutine (CurveAdd());
		}
	}

    /*
    private void OnCollisionEnter(Collision hit)
    {
		//if (col.gameObject.tag == "Ground") {

		//	bounceSound.Play();

		//}
       
		if (hit.gameObject.tag == "SoccerBall")
        {
			//dribbleSound.Play ();
            
			rb.AddForce(player.transform.forward * 0 + player.GetComponent<Rigidbody>().velocity * dribbleSpeed, ForceMode.Impulse);
			player.GetComponent<Rigidbody>().AddForce(-player.transform.forward * 100f,  ForceMode.Impulse);
            playerHasBall = true;
            Rigidbody rigidbody = hit.collider.attachedRigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hit.gameObject.transform.position - player.transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, player.transform.position, ForceMode.Impulse);
            }
        }
    }
    */

    void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.tag == "SoccerBall")
        {
            //dribbleSound.Play ();
            ball.transform.SetParent(holdBall.transform, true);
            ball.transform.SetParent(null, true);
            playerHasBall = false;

            animator.SetBool("isPassing", false);
            isPassing = false;
        }
    }

    IEnumerator DipAdd() 
	{
		rb.AddForce(-player.transform.up * 0.1f, ForceMode.Impulse);
		yield return new WaitForSeconds (1.5f);
		addDip = false;
	}

	IEnumerator CurveAdd()
    {
		rb.AddForce(-player.transform.right * Random.Range (curveMin, curveMax) * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds (1.5f);
		addCurve = false;
	}
}


