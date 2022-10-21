using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject thrash;
    [SerializeField] Vector3 projectile;
    [SerializeField] private float throwWaitMax = 2f;
    private Stack<GameObject> collectedThrashes;
    private float throwWait = 0f;


    void Start()
    {
        collectedThrashes = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().CollectedThrashes;
    }

    void Update()
    {
        throwWait += Time.deltaTime;

        if()
        {

        }

        if(throwWait > throwWaitMax)
        {
            throwWait = 0;
            GameObject firlat = Instantiate(thrash, transform.position, Quaternion.identity);
            Rigidbody rb = firlat.GetComponent<Rigidbody>();
            rb.AddForce( projectile, ForceMode.Impulse);

        }
    }
}
