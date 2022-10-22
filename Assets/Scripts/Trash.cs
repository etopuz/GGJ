using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashType
    {
        Plastic,
        Glass,
        Paper,
        Metal        
    }

    [SerializeField] TrashType tt;
    bool isThrowable = false;

    public bool IsThrowable
    {
        get=> isThrowable;
        set=> isThrowable = value;
    }

    public TrashType TType
    {
        get => tt;
    }

    void OnCollisionEnter(Collision other) 
    {
        //if(isThrowable && other.gameObject.CompareTag("zemin"))
        //{
        //    Destroy(GetComponent<Rigidbody>());
        //}    
    }
}
