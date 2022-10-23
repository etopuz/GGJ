using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    GameObject[] trashes;
    [SerializeField] private Transform thrashParent;
    [SerializeField] float throwWaitMax = 2f;
    [SerializeField] float throwSpeed = 2f;
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
            trashes[i] = allTrashParentTf.GetChild(i).gameObject;
        }
    }

    void Start()
    {
        cam = Camera.main;
        if(TryGetComponent<Character>(out Character c))
        {
            collectedThrashes = c.CollectedThrashes;
            isBoss = false;
        }
    }

    void Update()
    {
        if(isBoss)
        {
            BossShoot();
        }
        else
        {
            PlayerShoot();
        }
        
    }

    void ShootWithVelocity(GameObject gObj, Vector3 point)
    {
        Vector3 distance = point - transform.position;
        distance -= new Vector3(0,distance.y,0);
        gObj.GetComponent<Rigidbody>().velocity = distance.normalized * throwSpeed;
    }

    void PlayerShoot()
    {
        throwWait += Time.deltaTime;
        if (collectedThrashes != null && collectedThrashes.Count == 0)
        {
            return;
        }

        if(!Input.GetMouseButtonDown(0))
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
                gObj.GetComponent<Rigidbody>().useGravity = false;
                throwWait = 0;
                gObj.transform.position = transform.position;
                var trashComp = gObj.GetComponent<Trash>();
                trashComp.IsGatherable = false;
                trashComp.IsStopable = false;
                trashComp.CanMakePoisonousArea = false;
                ShootWithVelocity(gObj, hit.point);   
            }
        }
    }

    void BossShoot()
    {
        throwWait += Time.deltaTime;
        if (throwWait > throwWaitMax )
        {
            GameObject trash = trashes[Random.Range(0, trashes.Length)];
            throwWait = 0;
            GameObject firlat = Instantiate(trash, transform.position, trash.transform.rotation, thrashParent);
            firlat.GetComponent<Trash>().IsGatherable = false;
            ShootWithVelocity(firlat, new Vector3(Random.Range(-20f,20f),0,Random.Range(-20f, 20f)));
        }
    }
}
