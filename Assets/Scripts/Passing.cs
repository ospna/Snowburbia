using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Passing : MonoBehaviour
{
    private Passing[] otherPlayers;
    private Shooting[] players;

    private GameObject ball;
    private PlayerController playerMovement;
    private SphereCollider sphereCollider;

    [SerializeField]
    float passingForce;

    // Start is called before the first frame update
    void Start()
    {
        // using LINQ language to find every other player besides this
        otherPlayers = FindObjectsOfType<Passing>().Where(t => t != this).ToArray();
        ball = GameObject.FindGameObjectWithTag("SoccerBall");

        players = FindObjectsOfType<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Shooting player in players)
        {
            if (player.playerHasBall)
            {
                sphereCollider = player.GetComponent<SphereCollider>();
                sphereCollider.enabled = true;

                playerMovement = GetComponent<PlayerController>();

                Debug.DrawRay(player.transform.position, playerMovement.movementDirection * 10f, Color.red);
                Passing targetPlayer = FindPlayerInDirection(playerMovement.movementDirection);

                if (targetPlayer != null)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        player.passPlayed = true;
                        PassBallToPlayer(targetPlayer);
                    }
                }

            }

            if (!player.playerHasBall)
            {
                sphereCollider = player.GetComponent<SphereCollider>();
                sphereCollider.enabled = false;
            }
        }
    }

    private void PassBallToPlayer(Passing targetPlayer)
    {
        Vector3 direction = DirectionToPlayer(targetPlayer);
        ball.transform.SetParent(null);
        ball.GetComponent<Rigidbody>().AddForce(direction * passingForce);

        print("FORCE ADDED" + direction * passingForce);
    }

    Passing FindPlayerInDirection(Vector3 direction)
    {
        Passing selectedPlayer = null;
        float angle = Mathf.Infinity;

        foreach(Passing player in otherPlayers)
        {
            Vector3 directionToPlayer = DirectionToPlayer(player);

            Debug.DrawRay(transform.position, directionToPlayer, Color.blue);

            float playerAngle = Vector3.Angle(direction, directionToPlayer);

            if(playerAngle < angle)
            {
                selectedPlayer = player;
                angle = playerAngle;
            }
        }

        print(selectedPlayer.name);
        return selectedPlayer;
    }

    Vector3 DirectionToPlayer(Passing player)
    {
        return Vector3.Normalize(player.transform.position - ball.transform.position);
    }

}
