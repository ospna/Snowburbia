using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using static Assets.Scripts.Entities.Player;
using static Assets.Scripts.Entities.Ball;


public class Kicking : MonoBehaviour
{
    [Header("Pass and Shot Power Information")]
    public float passSpeed = 50;
    public float curveShootSpeed = 50;
    public float curveShotPower = 35;
    public float shootSpeedForward = 60;
    public float shootSpeedDown = 5;
    public float chipSpeedUp = 40;
    public float chipSpeedForward = 35;
    public float chipTorqueUp = 15;
    public float dribbleSpeed = 10;
    public float curveMin;
    public float curveMax;
    private float forceMagnitude;

    [Header("Game Objects")]
    //Player player;
    //InFieldPlayerFSM player;
    private GameObject player;
    private Animator animator;

    [Header("Shot Key Code Info")]
    public KeyCode passKeyCode = KeyCode.Mouse1;
    public KeyCode curveShotKeyCode = KeyCode.Z;
    public KeyCode shootKeyCode = KeyCode.Mouse0;
    public KeyCode chipShotKeyCode = KeyCode.C;

    [Header("References")]
    //public GameObject ball;
    //public Rigidbody rb;
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
    void Start()
    {
        //rb = ball.GetComponent<Rigidbody>();
        player = this.gameObject;
        animator = GetComponent<Animator>();
        //Player _player;
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

    private void OnTriggerEnter(Collider other)
    {
        holdBall.GetComponent<SphereCollider>().enabled = true;

        //unset the ball to is kinematic
        Ball.Instance.Owner = null;
        Ball.Instance.Rigidbody.isKinematic = false;
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "SoccerBall")
        {
            // Pass with Right Mouse Button Click
            if (Input.GetKeyDown(passKeyCode))
            {
                Ball.Instance.Rigidbody.AddForce(player.transform.forward * passSpeed, ForceMode.Impulse);
                //kickingSound.Play();
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isPassing", true);
                isPassing = true;

            }

            // Shoot with Left Mouse Button Click
            if (Input.GetKeyDown(shootKeyCode))
            {
                Ball.Instance.Rigidbody.AddForce(-player.transform.up * shootSpeedDown, ForceMode.Impulse);
                Ball.Instance.Rigidbody.AddForce(player.transform.forward * shootSpeedForward, ForceMode.Impulse);
                //kickingSound.Play();
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isShooting", true);
                isShooting = true;
            }

            // Curved Shot
            if (Input.GetKeyDown(curveShotKeyCode))
            {
                Ball.Instance.Rigidbody.AddForce(player.transform.forward * curveShootSpeed, ForceMode.Impulse);
                Ball.Instance.Rigidbody.AddForce(player.transform.up * curveShotPower, ForceMode.Impulse);
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
                Ball.Instance.Rigidbody.AddForce(player.transform.up * chipSpeedUp, ForceMode.Impulse);
                Ball.Instance.Rigidbody.AddForce(player.transform.forward * chipSpeedForward, ForceMode.Impulse);
                Ball.Instance.Rigidbody.AddTorque(-player.transform.right * chipTorqueUp, ForceMode.Impulse);
                //chipSound.Play();
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isChipping", true);
                isChipping = true;
            }
        }
    }

    void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.tag == "SoccerBall")
        {
            //unset the ball to is kinematic
            Ball.Instance.Owner = null;
            Ball.Instance.Rigidbody.isKinematic = false;

            //dribbleSound.Play ();
            //ball.transform.SetParent(null, true);
            //playerHasBall = false;

            //gm.GetComponent<SwitchPlayer>().enabled = true;

            //animator.SetBool("isPassing", false);
            //isPassing = false;
        }
    }

    IEnumerator DipAdd()
    {
        Ball.Instance.Rigidbody.AddForce(-player.transform.up * 0.1f, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        addDip = false;
    }

    IEnumerator CurveAdd()
    {
        Ball.Instance.Rigidbody.AddForce(-player.transform.right * Random.Range(curveMin, curveMax) * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        addCurve = false;
    }
}


