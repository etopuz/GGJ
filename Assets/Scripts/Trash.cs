using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("zemin"))
        {
            Destroy(GetComponent<Rigidbody>());
        }    
    }
}
