using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static int playerScore = 0;
    public static int aiScore = 0;
    public bool gamePaused = false;
    public bool matchOver;
    public CinemachineFreeLook cmCamera;

    public TextMeshProUGUI hometeamScoreText;
    public TextMeshProUGUI awayteamScoreText;
    public TextMeshProUGUI gametimerText;
    public TextMeshProUGUI winnerText;
    public string score;

    public GameObject ball, AI, GK, LB, RB, CM, player;
    public GameObject FinalScore;

    // A reference to the played minutes in the game
    public int Minutes { get; set; }

    // A reference to the played seconds in the game
    public int Seconds { get; set; }

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
        gametimerText.text = string.Format("{0:00}:{1:00}",
                Minutes.ToString("00"),
                Seconds.ToString("00"));
    }

    IEnumerator Match()
    {
        while (true)
        {
            yield return new WaitForSeconds(.05f);
            if (Minutes < 60 && !gamePaused)
            {
                //if seconds reaches 60
                //reset seconds, increment minutes
                if (Seconds >= 59)
                {
                    Seconds = 0;
                    ++Minutes;
                }
                else
                {
                    //increment seconds
                    ++Seconds;
                }

            }
            else if (Minutes == 60)
            {
                matchOver = true;
                Debug.Log("Game Over");
                break;
            }
        }

        if (matchOver == true)
        {

            Cursor.lockState = CursorLockMode.None;
            cmCamera.enabled = false;
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
