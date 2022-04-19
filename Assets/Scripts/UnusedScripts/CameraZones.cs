using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZones : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            virtualCamera.enabled = false;
        }
    }
}
