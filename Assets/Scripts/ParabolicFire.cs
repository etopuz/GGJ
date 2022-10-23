using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicFire : MonoBehaviour
{
    GameObject[] trashes;
    [SerializeField] float throwWaitMax = 2f;
    float maxSinir = 7f;
    float throwWait = 0f;
    Ray ray;
    RaycastHit hit;
    Camera cam;
    Stack<GameObject> collectedThrashes = null;
    bool isBoss = true;

    void Awake() 
    {
        //Load text from a JSON file (Assets/Resources/Text/jsonFile01.json)
        var allTrashParent = Resources.Load<GameObject>("AllTrashParent");
        var allTrashParentTf = allTrashParent.transform;
        int childCount = allTrashParentTf.childCount;
        trashes = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            trashes[i] =allTrashParentTf.GetChild(i).gameObject;
        }
    }

    void Start()
    {
        maxSinir = GameObject.FindObjectOfType<Character>().maxSinir;
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
            Shoot(firlat, new Vector3(Random.Range(-maxSinir, maxSinir), 0, Random.Range(-maxSinir, maxSinir)));
        }

        if(throwWaitMax>1f){
            throwWaitMax -= 0.2f;
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
                var trashComp = gObj.GetComponent<Trash>();
                trashComp.IsGatherable = false;
                trashComp.CanMakePoisonousArea = false;
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