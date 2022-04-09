using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] public Vector3 offset;

    public float smoothing = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("SoccerBall").transform;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = target.position + offset;
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothing);
    }
}
