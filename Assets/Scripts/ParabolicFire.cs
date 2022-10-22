using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicFire : MonoBehaviour
{
    [SerializeField] GameObject[] trashes;
    [SerializeField] float throwWaitMax = 2f;
    float throwWait = 0f;
    Ray ray;
    RaycastHit hit;
    Camera cam;
    Stack<GameObject> collectedThrashes = null;
    bool isBoss = true;

    void Start()
    {
        cam = Camera.main;
        if (TryGetComponent<Character>(out Character c))
        {
            collectedThrashes = c.CollectedThrashes;
            isBoss = false;
        }
    }

    void Update()
    {
        if (isBoss)
        {
            BossShoot();
        }
        else
        {
            PlayerShoot();
        }

    }

    void BossShoot()
    {
        throwWait += Time.deltaTime;
        if (throwWait > throwWaitMax)
        {
            GameObject trash = trashes[Random.Range(0, trashes.Length)];
            throwWait = 0;
            GameObject firlat = Instantiate(trash, transform.position, trash.transform.rotation);
            firlat.GetComponent<Trash>().IsGatherable = false;
            Shoot(firlat, new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f)));
        }

    }

    void PlayerShoot()
    {
        throwWait += Time.deltaTime;
        if (collectedThrashes != null && collectedThrashes.Count == 0)
        {
            return;
        }
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (throwWait > throwWaitMax && Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("zemin")))
        {
            if (collectedThrashes != null && collectedThrashes.TryPop(out GameObject gObj))
            {
                gObj.SetActive(true);
                gObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                throwWait = 0;
                gObj.transform.position = transform.position;
                gObj.GetComponent<Trash>().IsGatherable = false;
                Shoot(gObj, hit.point);
            }
        }
    }

    void Shoot(GameObject gObj, Vector3 point)
    {
        Vector3 distance = point - transform.position;
        float xSign = distance.x / Mathf.Abs(distance.x);
        float zSign = distance.z / Mathf.Abs(distance.z);
        float xx = distance.x * distance.x;
        float zz = distance.z * distance.z;
        float totalMagForXZ = xx + zz;
        float mag = new Vector2(distance.x, distance.z).magnitude;
        float newMag = mag * -Physics.gravity.y / 2;
        float yMag = Mathf.Sqrt(newMag);
        float xMag = Mathf.Sqrt(newMag * xx / totalMagForXZ);
        float zMag = Mathf.Sqrt(newMag * zz / totalMagForXZ);

        gObj.GetComponent<Rigidbody>().velocity = new Vector3(xSign * xMag, yMag, zSign * zMag);
        gObj.GetComponent<Rigidbody>().useGravity = true;
    }
}