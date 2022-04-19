using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRecenter : MonoBehaviour
{
    private CinemachineFreeLook cameraFL;

    // Start is called before the first frame update
    void Start()
    {
        cameraFL = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("CameraRecenter") == 1 || Input.GetButton("CameraRecenter"))
        {
            cameraFL.m_YAxisRecentering.m_enabled = true;
        }
        else
        {
            cameraFL.m_YAxisRecentering.m_enabled = false;
        }
    }
}
