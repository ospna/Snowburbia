using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int playerScore = 0;
    public static int aiScore = 0;
    public float timer = 100000;
    public bool gamePaused = false;

    public TextMeshProUGUI hometeamScoreText;
    public TextMeshProUGUI awayteamScoreText;
    public TextMeshProUGUI gametimerText;
    public TextMeshProUGUI winnerText;
    public string score;

    public bool matchOver;

    public GameObject ball, AI, player;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Match());
        ball = GameObject.FindGameObjectWithTag("SoccerBall");
        AI = GameObject.FindGameObjectWithTag("AI");
        player = GameObject.FindGameObjectWithTag("Player");
        //canvas.SetActive(false);
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
            ball.SetActive(false);
            canvas.SetActive(true);

            if (playerScore > aiScore)
            {
                winnerText.text = "The Player wins!";
            }
            else if (playerScore < aiScore)
            {
                winnerText.text = "The AI took home the 3 points.";
            }
            else if (playerScore == aiScore)
            {
                winnerText.text = "It's a bore draw.";
            }

            aiScore = 0;
            playerScore = 0;

        }
    }
}
