using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SocBal : MonoBehaviour
{
    GameManager gm;
    Vector3 startPos;
    //Vector3 AIstartPos;
    Rigidbody rigBod;

    //public CinemachineFreeLook cmFreeLook;
    private CinemachineVirtualCamera vCam;
    private GameObject Player;

    public GameObject PlayerIndicator;


    [SerializeField] public Transform playerFocus;

    public GameObject holdBall;
    public bool gameActive = true;
    public bool playerHasBall = true;
    public bool aiPlayerHasBall = true;

    public bool passPlayed = false;
    public bool aiPassPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");

        // holdBall = GameObject.FindGameObjectsWithTag("HoldBall");
        rigBod = GetComponent<Rigidbody>();
        startPos = transform.position;
        //AIstartPos = transform.position;
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
            gameActive = false;
            collision.isTrigger = false;
            //this.gameObject.SetActive(false);
            gm.gamePaused = true;
            Invoke("Reset", 5f);
        }

        if (collision.gameObject.tag == "AwayGoalZone")
        {
            GameManager.playerScore++;
            gameActive = false;
            //this.gameObject.SetActive(false);
            collision.isTrigger = false;
            gm.gamePaused = true;
            Invoke("Reset", 5f);
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
            transform.localPosition = new Vector3(0, 0.1f, 0.4f);
            //transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            playerHasBall = true;

            PlayerIndicator = GameObject.Find("Home_Team/Player/PlayerIndicator");
            PlayerIndicator.SetActive(true);

            //vCam.m_Follow = playerFocus;
            //cmFreeLook.m_LookAt = playerFocus;
            //cmFreeLook.m_Follow = playerFocus;

            gm.GetComponent<SwitchPlayer>().enabled = false;

            Player.GetComponent<PlayerController>().enabled = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HoldBall")
        {
            this.transform.SetParent(holdBall.transform, true);
            this.transform.SetParent(null, true);
            playerHasBall = false;
            PlayerIndicator = GameObject.Find("Home_Team/Player/PlayerIndicator");
            PlayerIndicator.SetActive(false);
            gm.GetComponent<SwitchPlayer>().enabled = true;
        }
       
    }

    public void Reset()
    {
    
        this.gameObject.SetActive(true);
        rigBod.velocity = Vector3.zero;
        this.transform.SetParent(holdBall.transform, true);
        this.transform.SetParent(null, true);
        transform.position = startPos;
        Player.transform.position = new Vector3(-2, 0, 0);

        gm.gamePaused = false;

        Collider awayCol = GameObject.FindGameObjectWithTag("AwayGoalZone").GetComponent<Collider>();
        awayCol.isTrigger = true;

        Collider homeCol = GameObject.FindGameObjectWithTag("HomeGoalZone").GetComponent<Collider>();
        homeCol.isTrigger = true;

        gameActive = true;
        playerHasBall = false;
    }
}
