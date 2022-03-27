using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour
{
    GameManager gm;
    Vector3 startPos;
    Rigidbody rb;

    public GameObject GoalText;
    public bool gameActive = true;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
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
        else if (collision.gameObject.tag == "AwayGoalZone")
        {
            GameManager.playerScore++;
            GoalText.SetActive(true);
            gameActive = false;
            this.gameObject.SetActive(false);
            gm.gamePaused = true;
            Invoke("Reset", 3f);
        }
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);

        rb.velocity = Vector3.zero;
        transform.position = startPos;
        gm.player.transform.position = new Vector3(-2, 0.2f, 0);
        gm.AI.transform.position = new Vector3(2, 0.2f, 0);
        GoalText.SetActive(false);
        gm.gamePaused = false;
        gameActive = true;
    }
}
