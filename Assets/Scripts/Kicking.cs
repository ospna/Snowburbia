using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicking : MonoBehaviour
{
	[Header("Pass and Shot Power Information")]
    public float passSpeed = 500;
    public float curveShootSpeed = 500;
	public float curveShotPower = 350;
	public float shootSpeedForward = 600;
    public float shootSpeedDown = 50;
	public float chipSpeedUp = 400;
	public float chipSpeedForward = 350;
    public float chipTorqueUp = 150;
    public float dribbleSpeed = 100;
    public float curveMin;
	public float curveMax;
    private float forceMagnitude;

    private float holdDownStartTime;

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

    GameManager gm;

    // Use this for initialization
    void Start ()
    {
		rb = ball.GetComponent<Rigidbody>();
		player = this.gameObject;
        animator = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (addDip == true)
        {
            StartCoroutine(DipAdd());
        }

        if (addCurve == true)
        {
            StartCoroutine(CurveAdd());
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "SoccerBall")
        {
            // Pass with Right Mouse Button Click
            if (Input.GetKeyDown(passKeyCode))
            {
                rb.AddForce(player.transform.forward * passSpeed, ForceMode.Impulse);
                //kickingSound.Play();
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isPassing", true);
                isPassing = true;

            }

            // Shoot with Left Mouse Button Click
            if (Input.GetKeyDown(shootKeyCode))
            {
                rb.AddForce(-player.transform.up * shootSpeedDown, ForceMode.Impulse);
                rb.AddForce(player.transform.forward * shootSpeedForward, ForceMode.Impulse);
                //kickingSound.Play();
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isShooting", true);
                isShooting = true;
            }

            /*FAILED POWER-UP
            // Pass with RMB
            if (Input.GetMouseButtonDown(1))
            {
                //rb.AddForce(player.transform.forward * passspeed * Time.deltaTime, ForceMode.Impulse);
                //kickingSound.Play();
                /*
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isPassing", true);
                isPassing = true;
                /

                holdDownStartTime = Time.time;
            }

            if (Input.GetMouseButtonUp(1))
            {
                float holdDownTime = Time.time - holdDownStartTime;
                rb.AddForce(player.transform.forward * CalculateHoldDownForce(holdDownTime));
                //CalculateHoldDownForce(holdDownTime)
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isPassing", true);
                isPassing = true;
            }

            // Shoot with LMB
            if (Input.GetMouseButtonDown(0))
            {
                rb.AddForce(-player.transform.up * shootSpeedDown * Time.deltaTime, ForceMode.Impulse);
                rb.AddForce(player.transform.forward * shootSpeedForward * Time.deltaTime, ForceMode.Impulse);
                //kickingSound.Play();
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isShooting", true);
                isShooting = true;
            }

            if (Input.GetMouseButtonUp(0))
            {

            }
            */

            // Curved Shot
            if (Input.GetKeyDown(curveShotKeyCode))
            {
                rb.AddForce(player.transform.forward * curveShootSpeed, ForceMode.Impulse);
                rb.AddForce(player.transform.up * curveShotPower, ForceMode.Impulse);
                //kickingSound.Play();
                addDip = true;
                addCurve = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isShooting", true);
                isShooting = true;
            }

            // Chip Shot
            if (Input.GetKeyDown(chipShotKeyCode))
            {
                rb.AddForce(player.transform.up * chipSpeedUp, ForceMode.Impulse);
                rb.AddForce(player.transform.forward * chipSpeedForward, ForceMode.Impulse);
                rb.AddTorque(-player.transform.right * chipTorqueUp , ForceMode.Impulse);
                //chipSound.Play();
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isChipping", true);
                isChipping = true;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        holdBall.GetComponent<SphereCollider>().enabled = true;
    }

    void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.tag == "SoccerBall")
        {
            //dribbleSound.Play ();
            ball.transform.SetParent(holdBall.transform, true);
            ball.transform.SetParent(null, true);
            playerHasBall = false;

            gm.GetComponent<SwitchPlayer>().enabled = true;

            //animator.SetBool("isPassing", false);
            //isPassing = false;
        }
    }

    /*      USED FOR PSYCHICS BASED DRIBBLING
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


        /*
    private float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 2f;
        float holdTimeNormalize = Mathf.Clamp01(holdTime / maxForceHoldDownTime);

        float force = holdTimeNormalize * 500f;
        return force;
    }
    */

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


