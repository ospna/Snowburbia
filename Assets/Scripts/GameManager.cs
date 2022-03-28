using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int playerScore = 0;
    public static int aiScore = 0;
    public float timer = 90;
    public bool gamePaused = false;
    public bool matchOver;

    public TextMeshProUGUI hometeamScoreText;
    public TextMeshProUGUI awayteamScoreText;
    public TextMeshProUGUI gametimerText;
    public TextMeshProUGUI winnerText;
    public string score;

    public GameObject ball, AI, GK, LB, RB, CM, player;
    public GameObject FinalScore;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Match());
        ball = GameObject.FindGameObjectWithTag("SoccerBall");
        AI = GameObject.FindGameObjectWithTag("AI");
        GK = GameObject.FindGameObjectWithTag("GK");
        LB = GameObject.FindGameObjectWithTag("LB");
        RB = GameObject.FindGameObjectWithTag("RB");
        CM = GameObject.FindGameObjectWithTag("CM");
        player = GameObject.FindGameObjectWithTag("Player");
        FinalScore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hometeamScoreText.text = "" + playerScore;
        awayteamScoreText.text = "" + aiScore;
        gametimerText.text = "" + timer;
    }

    IEnumerator Match()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (timer > 0 && !gamePaused)
            {
                timer--;
            }
            else if (timer == 0)
            {
                matchOver = true;
                Debug.Log("Game Over");
                break;
            }
        }

        if (matchOver == true)
        {
            AI.SetActive(false);
            player.SetActive(false);
            GK.SetActive(false);
            LB.SetActive(false);
            RB.SetActive(false);
            CM.SetActive(false);
            ball.SetActive(false);
            FinalScore.SetActive(true);

            if (playerScore > aiScore)
            {
                winnerText.text = "The Home team wins, what a victory!";
            }
            else if (playerScore < aiScore)
            {
                winnerText.text = "The Away team got away with the 3 points today.";
            }
            else if (playerScore == aiScore)
            {
                winnerText.text = "And that's that, it's a bore draw.";
            }

            //aiScore = 0;
            //playerScore = 0;

        }
    }
}
