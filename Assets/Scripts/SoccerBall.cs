using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SoccerBall : MonoBehaviour
{
    GameManager gm;
    Vector3 startPos;
    //Vector3 AIstartPos;
    Rigidbody rigBod;

    [SerializeField] private CinemachineFreeLook cmFreeLook;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject GK;
    [SerializeField] private GameObject LB;
    [SerializeField] private GameObject RB;
    [SerializeField] private GameObject CM;
    [SerializeField] private GameObject AI;
    [SerializeField] private GameObject AI_GK;
    [SerializeField] private GameObject AI_LB;
    [SerializeField] private GameObject AI_RB;
    [SerializeField] private GameObject AI_CM;


    [SerializeField] public Transform playerFocus;
    [SerializeField] public Transform lbFocus;
    [SerializeField] public Transform rbFocus;
    [SerializeField] public Transform cmFocus;
    [SerializeField] public Transform gkFocus;

    [SerializeField] public Transform ai_playerFocus;
    [SerializeField] public Transform ai_lbFocus;
    [SerializeField] public Transform ai_rbFocus;
    [SerializeField] public Transform ai_cmFocus;
    [SerializeField] public Transform ai_gkFocus;

    public GameObject GoalText;
    public GameObject holdBall;
    public GameObject lb_holdBall;
    public GameObject rb_holdBall;
    public GameObject cm_holdBall;
    public GameObject gk_holdBall;

    public GameObject ai_holdBall;
    public GameObject ai_lb_holdBall;
    public GameObject ai_rb_holdBall;
    public GameObject ai_cm_holdBall;
    public GameObject ai_gk_holdBall;

    public bool gameActive = true;
    public bool playerHasBall = false;
    public bool passPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        GK = GameObject.FindGameObjectWithTag("GK");
        LB = GameObject.FindGameObjectWithTag("LB");
        RB = GameObject.FindGameObjectWithTag("RB");
        CM = GameObject.FindGameObjectWithTag("CM");

        // holdBall = GameObject.FindGameObjectsWithTag("HoldBall");
        rigBod = GetComponent<Rigidbody>();
        startPos = transform.position;
        //AIstartPos = transform.position;
        GoalText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "HomeGoalZone")
        {
            GameManager.aiScore++;
            GoalText.SetActive(true);
            gameActive = false;
            this.gameObject.SetActive(false);
            gm.gamePaused = true;
            Invoke("Reset", 3f);
        }

        if (collision.gameObject.tag == "AwayGoalZone")
        {
            GameManager.playerScore++;
            GoalText.SetActive(true);
            gameActive = false;
            this.gameObject.SetActive(false);
            gm.gamePaused = true;
            Invoke("Reset", 3f);
        }

        /*
        if (collision.gameObject.tag == "LostPossession")
        {
            this.transform.SetParent(holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;

            cmFreeLook.m_LookAt = this.transform;
            cmFreeLook.m_Follow = this.transform;
        }
        */
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HoldBall")
        {
            transform.parent = holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = playerFocus;
            cmFreeLook.m_Follow = playerFocus;

            Player.GetComponent<PlayerController>().enabled = true;
            CM.GetComponent<PlayerController>().enabled = false;
            LB.GetComponent<PlayerController>().enabled = false;
            RB.GetComponent<PlayerController>().enabled = false;
            GK.GetComponent<PlayerController>().enabled = false;
        }

        if (other.gameObject.tag == "CM_HoldBall")
        {
            transform.parent = cm_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = cmFocus;
            cmFreeLook.m_Follow = cmFocus;

            Player.GetComponent<PlayerController>().enabled = false;
            CM.GetComponent<PlayerController>().enabled = true;
            LB.GetComponent<PlayerController>().enabled = false;
            RB.GetComponent<PlayerController>().enabled = false;
            GK.GetComponent<PlayerController>().enabled = false;
        }

        if (other.gameObject.tag == "LB_HoldBall")
        {
            transform.parent = lb_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = lbFocus;
            cmFreeLook.m_Follow = lbFocus;

            Player.GetComponent<PlayerController>().enabled = false;
            CM.GetComponent<PlayerController>().enabled = false;
            LB.GetComponent<PlayerController>().enabled = true;
            RB.GetComponent<PlayerController>().enabled = false;
            GK.GetComponent<PlayerController>().enabled = false;
        }

        if (other.gameObject.tag == "RB_HoldBall")
        {
            transform.parent = rb_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = rbFocus;
            cmFreeLook.m_Follow = rbFocus;

            Player.GetComponent<PlayerController>().enabled = false;
            CM.GetComponent<PlayerController>().enabled = false;
            LB.GetComponent<PlayerController>().enabled = false;
            RB.GetComponent<PlayerController>().enabled = true;
            GK.GetComponent<PlayerController>().enabled = false;
        }

        if (other.gameObject.tag == "GK_HoldBall")
        {
            transform.parent = gk_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = gkFocus;
            cmFreeLook.m_Follow = gkFocus;

            Player.GetComponent<PlayerController>().enabled = false;
            CM.GetComponent<PlayerController>().enabled = false;
            LB.GetComponent<PlayerController>().enabled = false;
            RB.GetComponent<PlayerController>().enabled = false;
            GK.GetComponent<PlayerController>().enabled = true;
        }

        // -------------------------- AI --------------------------------------- //

        if (other.gameObject.tag == "AI_HoldBall")
        {
            transform.parent = ai_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = ai_playerFocus;
            cmFreeLook.m_Follow = ai_playerFocus;

            AI.GetComponent<PlayerController>().enabled = true;
            AI_CM.GetComponent<PlayerController>().enabled = false;
            AI_LB.GetComponent<PlayerController>().enabled = false;
            AI_RB.GetComponent<PlayerController>().enabled = false;
            AI_GK.GetComponent<PlayerController>().enabled = false;
        }

        if (other.gameObject.tag == "AI_CM_HoldBall")
        {
            transform.parent = ai_cm_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = ai_cmFocus;
            cmFreeLook.m_Follow = ai_cmFocus;

            AI.GetComponent<PlayerController>().enabled = false;
            AI_CM.GetComponent<PlayerController>().enabled = true;
            AI_LB.GetComponent<PlayerController>().enabled = false;
            AI_RB.GetComponent<PlayerController>().enabled = false;
            AI_GK.GetComponent<PlayerController>().enabled = false;
        }

        if (other.gameObject.tag == "AI_LB_HoldBall")
        {
            transform.parent = ai_lb_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = ai_lbFocus;
            cmFreeLook.m_Follow = ai_lbFocus;

            AI.GetComponent<PlayerController>().enabled = false;
            AI_CM.GetComponent<PlayerController>().enabled = false;
            AI_LB.GetComponent<PlayerController>().enabled = true;
            AI_RB.GetComponent<PlayerController>().enabled = false;
            AI_GK.GetComponent<PlayerController>().enabled = false;
        }

        if (other.gameObject.tag == "AI_RB_HoldBall")
        {
            transform.parent = ai_rb_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = ai_rbFocus;
            cmFreeLook.m_Follow = ai_rbFocus;

            AI.GetComponent<PlayerController>().enabled = false;
            AI_CM.GetComponent<PlayerController>().enabled = false;
            AI_LB.GetComponent<PlayerController>().enabled = false;
            AI_RB.GetComponent<PlayerController>().enabled = true;
            AI_GK.GetComponent<PlayerController>().enabled = false;
        }

        if (other.gameObject.tag == "AI_GK_HoldBall")
        {
            transform.parent = ai_gk_holdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            cmFreeLook.m_LookAt = ai_gkFocus;
            cmFreeLook.m_Follow = ai_gkFocus;

            AI.GetComponent<PlayerController>().enabled = false;
            AI_CM.GetComponent<PlayerController>().enabled = false;
            AI_LB.GetComponent<PlayerController>().enabled = false;
            AI_RB.GetComponent<PlayerController>().enabled = false;
            AI_GK.GetComponent<PlayerController>().enabled = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HoldBall")
        {
            this.transform.SetParent(holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;

            transform.localRotation = Quaternion.identity;

        }

        if (other.gameObject.tag == "CM_HoldBall")
        {
            this.transform.SetParent(cm_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

        if (other.gameObject.tag == "LB_HoldBall")
        {
            this.transform.SetParent(lb_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

        if (other.gameObject.tag == "RB_HoldBall")
        {
            this.transform.SetParent(rb_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

        if (other.gameObject.tag == "GK_HoldBall")
        {
            this.transform.SetParent(gk_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

        // -------------------------- AI --------------------------------------- //

        if (other.gameObject.tag == "AI_HoldBall")
        {
            this.transform.SetParent(ai_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

        if (other.gameObject.tag == "AI_M_HoldBall")
        {
            this.transform.SetParent(ai_cm_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

        if (other.gameObject.tag == "AI_LB_HoldBall")
        {
            this.transform.SetParent(ai_lb_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

        if (other.gameObject.tag == "AI_RB_HoldBall")
        {
            this.transform.SetParent(ai_rb_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

        if (other.gameObject.tag == "AI_GK_HoldBall")
        {
            this.transform.SetParent(ai_gk_holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
        }

    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
        rigBod.velocity = Vector3.zero;
        this.transform.SetParent(holdBall.transform, true);
        this.transform.SetParent(null, true);
        transform.position = startPos;
        gm.player.transform.position = new Vector3(-2, 0, 0);
        gm.GK.transform.position = new Vector3(-18, 0, 0);
        gm.LB.transform.position = new Vector3(-12, 0, 4);
        gm.RB.transform.position = new Vector3(-12, 0, -4);
        gm.CM.transform.position = new Vector3(-6, 0, 0);
        gm.AI.transform.position = new Vector3(2, 0, 0);
        GoalText.SetActive(false);
        gm.gamePaused = false;
        gameActive = true;
        playerHasBall = false;
    }
}
