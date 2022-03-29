using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillBallAnim : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 2.5f, 0 * Time.deltaTime);
    }
}
