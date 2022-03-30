using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldBall : MonoBehaviour
{
    public GameObject ball;
    //public GameObject holdBall;
    public bool playerHasBall = true;
    public bool passPlayed = false;
    Rigidbody rb;

    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        //ball = GameObject.FindGameObjectWithTag("SoccerBall");
        //rb = GetComponent<Rigidbody>();
    }

    /*
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SoccerBall")
        {
            ball.transform.parent = transform;
            ball.transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            //ball.GetComponent<Rigidbody>().isKinematic = true;
            //ball.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            playerHasBall = true;
            passPlayed = false;
}
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "SoccerBall")
        {
            ball.transform.SetParent(transform, true);
            ball.transform.SetParent(null, true);
            //ball.GetComponent<Rigidbody>().isKinematic = false;
            //ball.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            playerHasBall = false;
            passPlayed = true;
        }
    }
    */
}
