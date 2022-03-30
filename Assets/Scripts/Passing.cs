using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Passing : MonoBehaviour
{
    private Passing[] otherPlayers;
    private Shooting[] players;
    private HoldBall[] possesion;

    private GameObject ball;
    private PlayerController playerMovement;
    private SphereCollider sphereCollider;

    public GameObject holdBall;

    [SerializeField]
    float passingForce;

    // Start is called before the first frame update
    void Start()
    {
        // using LINQ language to find every other player besides this
        otherPlayers = FindObjectsOfType<Passing>().Where(t => t != this).ToArray();
        ball = GameObject.FindGameObjectWithTag("SoccerBall");

        players = FindObjectsOfType<Shooting>();
        possesion = FindObjectsOfType<HoldBall>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (HoldBall pos in possesion)
        {
            if (pos.playerHasBall)
            {
                sphereCollider = pos.GetComponent<SphereCollider>();
                sphereCollider.enabled = true;

                playerMovement = GetComponent<PlayerController>();

                Debug.DrawRay(pos.transform.position, playerMovement.movementDirection * 10f, Color.red);
                Passing targetPlayer = FindPlayerInDirection(playerMovement.movementDirection);

                if (targetPlayer != null)
                {
                    if (Input.GetButtonDown("Fire1") )
                    {
                        pos.passPlayed = true;
                        PassBallToPlayer(targetPlayer);
                    }
                }

            }

            if (!pos.playerHasBall)
            {
                sphereCollider = pos.GetComponent<SphereCollider>();
                sphereCollider.enabled = false;
            }
        }
    }

    private void PassBallToPlayer(Passing targetPlayer)
    {
        Vector3 direction = DirectionToPlayer(targetPlayer);
        ball.transform.SetParent(null);
        //ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Rigidbody>().AddForce(direction * passingForce);

        print("FORCE ADDED" + direction * passingForce);
    }

    Passing FindPlayerInDirection(Vector3 direction)
    {
        /*
        var closestAngle = otherPlayers
            .OrderBy(t => Vector3.Angle(direction, DirectionToPlayer(t)))
            .FirstOrDefault();

        return closestAngle;
        */

        Passing selectedPlayer = null;
        float angle = Mathf.Infinity;

        foreach(Passing player in otherPlayers)
        {
            var directionToPlayer = DirectionToPlayer(player);

            Debug.DrawRay(transform.position, directionToPlayer, Color.blue);

            var playerAngle = Vector3.Angle(direction, directionToPlayer);

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
