using System.Linq;
using UnityEngine;

public class PassingTest : MonoBehaviour
{
    private PassingTest[] allOtherPlayers;
    private SoccerBall ball;
    public float passForce = 100f;

    public GameObject holdBall;

    public bool playerHasBall = true;
    public bool passPlayed = false;

    RaycastHit hit;

    private void Awake()
    {
        allOtherPlayers = FindObjectsOfType<PassingTest>().Where(t => t != this).ToArray();
        ball = FindObjectOfType<SoccerBall>();
    }

    private void FixedUpdate()
    {
        if (HoldingBall())
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical);
            Debug.DrawRay(transform.position, direction * 10f, Color.red);

            var targetPlayer = FindPlayerInDirection(direction);

            if (targetPlayer != null)
            {
                if (Input.GetButton("Fire1"))
                    PassBallToPlayer(targetPlayer);
            }

            /*
            if (Physics.Raycast(player.transform.position, fwd, out hit, 10))
            {
                Debug.DrawRay(transform.position, direction * 10f, Color.red);

                //var targetPlayer = FindPlayerInDirection(direction);

                if (targetPlayer != null)
                {
                    if (Input.GetButton("Fire1"))
                        PassBallToPlayer(targetPlayer);
                }
            }
            */
        }
    }

    private void PassBallToPlayer(PassingTest targetPlayer)
    {
        var direction = DirectionTo(targetPlayer);
        ball.transform.SetParent(holdBall.transform, true);
        ball.transform.SetParent(null, true);
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Rigidbody>().AddForce(direction * passForce);
        playerHasBall = false;
        passPlayed = true;

        //rb.AddForce(player.transform.forward * shootspeed * Time.deltaTime, ForceMode.Impulse);
    }

    private Vector3 DirectionTo(PassingTest player)
    {
        return Vector3.Normalize(player.transform.position - ball.transform.position);
    }

    private PassingTest FindPlayerInDirection(Vector3 direction)
    {
        PassingTest selectedPlayer = null;
        float angle = Mathf.Infinity;

        foreach (PassingTest player in allOtherPlayers)
        {
            if (Physics.Raycast(player.transform.position, transform.TransformDirection(Vector3.forward), out hit, 10))
            {
                var directionToPlayer = DirectionTo(player);

                Debug.DrawRay(player.transform.position, directionToPlayer, Color.blue);

                var playerAngle = Vector3.Angle(direction, directionToPlayer);

                if (playerAngle < angle)
                {
                    selectedPlayer = player;
                    angle = playerAngle;
                }
            }
        }

        print(selectedPlayer.name);
        return selectedPlayer;

        /*
        var closestAngle = allOtherPlayers
            .OrderBy(t => Vector3.Angle(direction, DirectionTo(t)))
            .FirstOrDefault();

        return closestAngle;
        */

        // Non-LINQ Version
        //Passing selectedPlayer = null;
        //float angle = Mathf.Infinity;
        //foreach(var player in allOtherPlayers)
        //{
        //    var directionToPlayer = DirectionTo(player);
        //    Debug.DrawRay(transform.position, directionToPlayer, Color.blue);
        //    var playerAngle = Vector3.Angle(direction, directionToPlayer);
        //    if (playerAngle < angle)
        //    {
        //        selectedPlayer = player;
        //        angle = playerAngle;
        //    }
        //}
        //return selectedPlayer;
    }

    private bool HoldingBall()
    {
        return transform.childCount > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        SoccerBall ball = other.GetComponent<SoccerBall>();
        if (ball != null)
        {
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //ball.GetComponent<Rigidbody>().isKinematic = true;
            //ball.transform.SetParent(transform);
            ball.transform.parent = holdBall.transform;
            holdBall.transform.localPosition = new Vector3(0, 0, 0.2f);
        }
    }
}