using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchPlayer : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public GameObject player;
    public List<GameObject> teammates;
    public int whichTeammate;
    public int wc;

    //private CharacterController characterController;
    //public GameObject PlayerIndicator;

    [Header("Shot Key Code Info")]
    public KeyCode switchKeyCode = KeyCode.Tab;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null && teammates.Count >= 1)
        {
            player = teammates[0];
        }
        Swap();


        //teamMates = CollectTeamMates();
        //SwitchPlayer = GameObject.FindGameObjectWithTag("SwitchPlayer");
        /*
        CM = GameObject.FindGameObjectWithTag("CM");
        LB = GameObject.FindGameObjectWithTag("LB");
        RB = GameObject.FindGameObjectWithTag("RB");
        GK = GameObject.FindGameObjectWithTag("GK");
        */

        //PlayerIndicator = GameObject.FindGameObjectWithTag("PlayerIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        // Switch SwitchPlayer with Tab
        if (Input.GetKeyDown(switchKeyCode))
        {
            /*
            if (Vector3.Distance(teammates[whichTeammate].transform.position, player.transform.position) > 5)
            {
                return;
            }
            else
            {
            */
            if (whichTeammate == 0)
            {
                whichTeammate = teammates.Count - 1;
                player.transform.GetChild(8).gameObject.SetActive(false);
            }
            else
            {
                whichTeammate -= 1;
                player.transform.GetChild(8).gameObject.SetActive(false);
            }
            Swap();

            /*
            wc = whichTeammate;
            if(whichTeammate >= teammates.Count - 1)
            {
                whichTeammate = 0;
            }
            else
            {
                whichTeammate += 1;
            }

            if (Vector3.Distance(teammates[whichTeammate].transform.position, player.transform.position) > 5)
            {
                whichTeammate = wc;
                return;
            }
            player.transform.GetChild(8).gameObject.SetActive(false);
            Swap();
            */
        }
    }

    public void Swap()
    {
        player = teammates[whichTeammate];
        player.GetComponent<PlayerController>().enabled = true;
        //player.transform.GetChild(8).gameObject.SetActive(false);

        for (int i = 0; i < teammates.Count; i++)
        {
            if (teammates[i] != player)
            {
                teammates[i].GetComponent<PlayerController>().enabled = false;
                player.transform.GetChild(8).gameObject.SetActive(true);
            }
        }

        //vCam.m_Follow = player.transform;
    }

    /*
    Transform GetClosestTeammate(Transform[] teammate)
    {
            for (int i = 0; i < teammate.Length; i++)
            {
                Transform bestTarget = null;
                float closestDistanceSqr = Mathf.Infinity;
                Vector3 currentPosition = transform.position;
                foreach (Transform potentialTarget in teammate)
                {
                    Vector3 directionToTarget = potentialTarget.position - currentPosition;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToTarget;
                        bestTarget = potentialTarget;
                    }
                }
                return bestTarget;
            }
    }
    */

}
