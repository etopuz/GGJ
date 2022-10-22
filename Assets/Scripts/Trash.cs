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
    public bool isGatherable = true;
    public bool isStopable = true;

    public bool IsGatherable
    {
        get=> isGatherable;
        set=> isGatherable = value;
    }

    public bool IsStopable
    {
        get => isStopable;
        set => isStopable = value;
    }

    public TrashType TType
    {
        get => tt;
    }

    void OnCollisionEnter(Collision other) 
    {
        if(!IsGatherable  && isStopable && other.gameObject.CompareTag("zemin") && GetComponent<Rigidbody>().velocity.y <=0.01f)
        {
            Debug.Log("a");
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;     
            isGatherable = true;   
        }    
    }
}
