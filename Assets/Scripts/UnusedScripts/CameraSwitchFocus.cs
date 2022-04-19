using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraSwitchFocus : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cmFreeLook;

    [SerializeField] public Transform playerFocus;
    [SerializeField] public Transform lbFocus;
    [SerializeField] public Transform rbFocus;
    [SerializeField] public Transform cmFocus;
    [SerializeField] public Transform gkFocus;
    [SerializeField] public Transform ballFocus;

    // Start is called before the first frame update
    void Start()
    {
        cmFreeLook = GetComponent<CinemachineFreeLook>();
        /*
        playerFocus = GetComponent<Transform>();
        lbFocus = FindGameObjectsWithTag("LB_HoldBall");
        rbFocus = FindGameObjectsWithTag("RB_HoldBall");
        cmFocus = FindGameObjectsWithTag("CM_HoldBall");
        gkFocus = FindGameObjectsWithTag("GK_HoldBall");
        */

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HoldBall"))
        {
            cmFreeLook.m_LookAt = playerFocus;
            cmFreeLook.m_Follow = playerFocus;
        }

        if (other.gameObject.CompareTag("LB_HoldBall"))
        {
            cmFreeLook.m_LookAt = lbFocus;
            cmFreeLook.m_Follow = lbFocus;
        }

        if (other.gameObject.CompareTag("RB_HoldBall"))
        {
            cmFreeLook.m_LookAt = rbFocus;
            cmFreeLook.m_Follow = rbFocus;
        }

        if (other.gameObject.CompareTag("CM_HoldBall"))
        {
            cmFreeLook.m_LookAt = cmFocus;
            cmFreeLook.m_Follow = cmFocus;
        }

        if (other.CompareTag("GK_HoldBall"))
        {
            cmFreeLook.m_LookAt = gkFocus;
            cmFreeLook.m_Follow = gkFocus;
        }

    }

    private void OnTriggerExit(Collider exit)
    {
        if (exit.CompareTag("HoldBall"))
        {
            cmFreeLook.m_LookAt = ballFocus;
            cmFreeLook.m_Follow = ballFocus;
        }

        if (exit.CompareTag("LB_HoldBall"))
        {
            cmFreeLook.m_LookAt = ballFocus;
            cmFreeLook.m_Follow = ballFocus;
        }

        if (exit.CompareTag("RB_HoldBall"))
        {
            cmFreeLook.m_LookAt = ballFocus;
            cmFreeLook.m_Follow = ballFocus;
        }

        if (exit.CompareTag("CM_HoldBall"))
        {
            cmFreeLook.m_LookAt = ballFocus;
            cmFreeLook.m_Follow = ballFocus;
        }

        if (exit.CompareTag("GK_HoldBall"))
        {
            cmFreeLook.m_LookAt = ballFocus;
            cmFreeLook.m_Follow = ballFocus;
        }
    }
}
