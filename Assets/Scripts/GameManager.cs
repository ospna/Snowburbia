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
    public float timer = 0;
    public float min, sec;
    public float timerSpeed = 1f;
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
        //gametimerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    IEnumerator Match()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (timer < 90 && !gamePaused)
            {
                timer++;
                /*
                timer += Time.fixedDeltaTime * timerSpeed;
                min = (int)(timer / 60 % 60);
                sec = (int)(timer % 60);

                //gametimerText.text = string.Format("{0:00}:{1:00}", min, sec);
                 */
                //yield return null;
               
            }
            else if (timer == 90)
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
