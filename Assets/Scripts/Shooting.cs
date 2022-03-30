using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	[Header("Shot Power Information")]
	public float shootspeed = 1000f;
	public float curveShotPowerUp = 400;
	public float powerShotSpeedForward = 250;
	public float powerShotSpeedDown = 250;
	public float lobSpeedUp = 1000;
	public float lobSpeedForward = 500;
	public float curveMin;
	public float curveMax;
	public float lobTorqueUp = 500;
	public float dribbleSpeed = 100;

    [SerializeField]
    private float forceMagnitude;

    [Header("Shot Key Code Info")]
	public KeyCode lobShotKeyCode = KeyCode.V;
	public KeyCode normalShotKeyCode = KeyCode.X;
	public KeyCode curveShotKeyCode = KeyCode.Z;
	public KeyCode powerShotKeyCode = KeyCode.C;

	[Header("References")]
	public GameObject player;
	public GameObject ball;
	//public Camera playercamera;
	public Rigidbody rb;
    public GameObject holdBall;

    /*
	[Header("Audio")]
	public AudioSource footballSound;
	public AudioSource bounceSound;
	public AudioSource dribbleSound;
    */

    [Header("Bool")]
	public bool isKicked = false;
	public bool addCurve = false;
	public bool addDip = false;
    public bool playerHasBall = false;

    // Use this for initialization
    void Start () {
		rb = ball.GetComponent<Rigidbody>();
		player = this.gameObject;
	}

    private void OnTriggerEnter(Collider other)
    {
        holdBall.GetComponent<SphereCollider>().enabled = true;
    }

    void OnTriggerStay(Collider other) {
		if (Input.GetKeyDown(normalShotKeyCode) && other.gameObject.tag == "SoccerBall")
        {
			rb.AddForce(player.transform.forward * shootspeed * Time.deltaTime, ForceMode.Impulse);
			//footballSound.Play ();
			//isKicked = true;
			addDip = true;
            holdBall.GetComponent<SphereCollider>().enabled = false;
		}

		if (Input.GetKeyDown(curveShotKeyCode) && other.gameObject.tag == "SoccerBall")
        {
			rb.AddForce(player.transform.forward * shootspeed * Time.deltaTime, ForceMode.Impulse);
			rb.AddForce(player.transform.up * curveShotPowerUp * Time.deltaTime, ForceMode.Impulse);
			//footballSound.Play ();
			addDip = true;
			addCurve = true;
            holdBall.GetComponent<SphereCollider>().enabled = false;
        }

		if (Input.GetKeyDown(powerShotKeyCode) && other.gameObject.tag == "SoccerBall")
        {
			rb.AddForce(-player.transform.up * powerShotSpeedDown * Time.deltaTime, ForceMode.Impulse);
			rb.AddForce(player.transform.forward * powerShotSpeedForward * Time.deltaTime, ForceMode.Impulse);
			//footballSound.Play ();
			addDip = true;
            holdBall.GetComponent<SphereCollider>().enabled = false;
        }

		if (Input.GetKeyDown(lobShotKeyCode) && other.gameObject.tag == "SoccerBall")
        {
			rb.AddForce(player.transform.up * lobSpeedUp * Time.deltaTime, ForceMode.Impulse);
			rb.AddForce(player.transform.forward * lobSpeedForward * Time.deltaTime, ForceMode.Impulse);
			rb.AddTorque(-player.transform.right * lobTorqueUp * Time.deltaTime, ForceMode.Impulse);
			//footballSound.Play ();
			addDip = true;
            holdBall.GetComponent<SphereCollider>().enabled = false;
        }
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			Application.Quit ();
		}

        /*
		if (isKicked == true)
        {   // Curve force added each frame
			StartCoroutine(iskickedStopTimer());
		}
        */

		if (addDip == true)
        {
			StartCoroutine (DipAdd ());
		}

		if (addCurve == true)
        {
			StartCoroutine (CurveAdd());
		}
	}

    private void OnCollisionEnter(Collision hit)
    {
		//if (col.gameObject.tag == "Ground") {

		//	bounceSound.Play();

		//}
        /*

		if (hit.gameObject.tag == "SoccerBall")
        {
			//dribbleSound.Play ();
            /*
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
        */
    }


    void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.tag == "SoccerBall")
        {
            //dribbleSound.Play ();
            ball.transform.SetParent(holdBall.transform, true);
            ball.transform.SetParent(null, true);
            playerHasBall = false;
        }
    }

    /*
	IEnumerator iskickedStopTimer()
    {
		rb.AddForce (-player.transform.right* Random.Range (0.3f, 0.7f), ForceMode.Impulse);
		rb.AddForce (player.transform.right* Random.Range (0.6f, 1f), ForceMode.Impulse);
		rb.AddForce (-player.transform.right* Random.Range (0.4f, 0.8f), ForceMode.Impulse);
		rb.AddForce (player.transform.up * 0.5f, ForceMode.Impulse);
		rb.AddForce (player.transform.right* Random.Range (0.4f, 0.6f), ForceMode.Impulse);
		rb.freezeRotation = true;
		yield return new WaitForSeconds (1.5f);
		rb.freezeRotation = false;
		isKicked = false;
	}
    */

    IEnumerator DipAdd() 
	{
		rb.AddForce (-player.transform.up * 0.1f, ForceMode.Impulse);
		yield return new WaitForSeconds (1.5f);
		addDip = false;
	}

	IEnumerator CurveAdd()
    {
		rb.AddForce(-player.transform.right* Random.Range (curveMin, curveMax) * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds (1.5f);
		addCurve = false;
	}
}


