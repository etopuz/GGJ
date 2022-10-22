using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicFire : MonoBehaviour
{
    [SerializeField] GameObject trash;
    [SerializeField] float throwWaitMax = 2f;
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
        if(throwWait > throwWaitMax && Physics.Raycast(ray, out hit))
        {
            throwWait = 0;
            GameObject firlat = Instantiate(trash, transform.position, Quaternion.identity);
            ShootWithGravity(firlat, hit.point);
        }
    }

    void ShootWithGravity(GameObject gObj, Vector3 point)
    {
        Vector3 distance = point - transform.position;
        float xSign = distance.x/Mathf.Abs(distance.x);
        float zSign = distance.z / Mathf.Abs(distance.z);
        float xx = distance.x * distance.x;
        float zz = distance.z * distance.z;
        float totalMagForXZ = xx + zz;
        float mag = new Vector2(distance.x, distance.z).magnitude;
        float newMag = mag * -Physics.gravity.y / 2;
        float yMag = Mathf.Sqrt(newMag);
        float xMag = Mathf.Sqrt(newMag*xx/totalMagForXZ);
        float zMag = Mathf.Sqrt(newMag * zz / totalMagForXZ);

        gObj.GetComponent<Rigidbody>().velocity = new Vector3(xSign*xMag,yMag, zSign* zMag);

    }
}