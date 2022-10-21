using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10);
    [SerializeField] private Vector3 maxPos = new Vector3(30, 10, 30);
    [SerializeField] private Vector3 minPos = new Vector3(-30, 10, -30);
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Transform target;
    private Vector3 vecZero = Vector3.zero;
    private Vector3 lastPosInArea = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 selfPos = transform.position;
        if(targetPos.x > maxPos.x)
        {
            targetPos = new Vector3(maxPos.x,targetPos.y,targetPos.z);
        }
        if(targetPos.x < minPos.x)
        {
            targetPos = new Vector3(minPos.x, targetPos.y, targetPos.z);
        }
        if (targetPos.z > maxPos.z)
        {
            targetPos = new Vector3(targetPos.x, targetPos.y, maxPos.z);
        }
        if (targetPos.z < minPos.z)
        {
            targetPos = new Vector3(targetPos.x, targetPos.y, minPos.z);
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vecZero, smoothTime);
    }
}
