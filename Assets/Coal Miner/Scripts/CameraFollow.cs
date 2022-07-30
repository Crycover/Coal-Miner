using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    public Vector3 offSet;


    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offSet, Time.deltaTime * 50f);
    }
}
