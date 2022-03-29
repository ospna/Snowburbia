using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hOLDbALL : MonoBehaviour
{
    public GameObject ball;
    public GameObject holdBall;
    public bool playerHasBall = false;

    private SphereCollider sphereCollider;
    private BoxCollider boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SoccerBall")
        {
            ball.transform.parent = holdBall.transform;
            holdBall.transform.localPosition = new Vector3(0, 0, 0);
            //ball.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            playerHasBall = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "SoccerBall")
        {
            ball.transform.SetParent(holdBall.transform, true);
            ball.transform.SetParent(null, true);
            //ball.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            playerHasBall = false;
        }
    }
}
