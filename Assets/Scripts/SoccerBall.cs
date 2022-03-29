using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour
{
    GameManager gm;
    Vector3 startPos;
    //Vector3 startScale;
    //Vector3 AIstartPos;
    Rigidbody rb;

    public GameObject GoalText;
    public GameObject holdBall;
    public bool gameActive = true;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        //startScale = transform.localScale;
        //AIstartPos = transform.position;
        GoalText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
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
            //this.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }

        if (collision.gameObject.tag == "AwayGoalZone")
        {
            GameManager.playerScore++;
            GoalText.SetActive(true);
            gameActive = false;
            this.gameObject.SetActive(false);
            gm.gamePaused = true;
            Invoke("Reset", 3f);
            //this.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }

        if (collision.gameObject.tag == "HoldBall")
        {
            this.transform.SetParent(holdBall.transform, true);
            this.transform.SetParent(null, true);
            //this.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
        rb.velocity = Vector3.zero;
        transform.SetParent(holdBall.transform, true);
        transform.SetParent(null, true);
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
    }
}
