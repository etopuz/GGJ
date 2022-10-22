using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    [SerializeField] GameObject trash;
    [SerializeField] float throwWaitMax = 2f;
    [SerializeField] float throwSpeed = 2f;
    float throwWait = 0f;
    Ray ray;
    RaycastHit hit;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        throwWait += Time.deltaTime;
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (throwWait > throwWaitMax && Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("zemin")))
        {
            throwWait = 0;
            GameObject firlat = Instantiate(trash, transform.position, Quaternion.identity);
            firlat.GetComponent<Trash>().IsGatherable = false;
            ShootWithVelocity(firlat, hit.point);
        }
    }

    void ShootWithVelocity(GameObject gObj, Vector3 point)
    {
        Vector3 distance = point - transform.position;
        gObj.GetComponent<Rigidbody>().velocity = distance.normalized * throwSpeed;

    }
}
