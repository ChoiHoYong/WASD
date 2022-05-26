using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMove : MonoBehaviour
{
    Vector3 destination = new Vector3(960, 540, 0); 

    void Update()
    {
        Vector3 speed = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref speed, 0.1f);
    }
}
