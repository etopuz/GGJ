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
    static float maxWaitTime = 5f;
    
    static GameObject poisonousArea;

    bool isGatherable = true;
    bool isStopable = true;    
    bool canMakePoisonousArea = true;
    float waitTime = 0;
    

    public bool IsGatherable
    {
        get=> isGatherable;
        set=> isGatherable = value;
    }

    public bool CanMakePoisonousArea
    {
        get => canMakePoisonousArea;
        set => canMakePoisonousArea = value;
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

    private void Awake() {
        poisonousArea = Resources.Load<GameObject>("PoisonousArea");
    }

    void OnCollisionEnter(Collision other) 
    {
        if(!IsGatherable  && isStopable && other.gameObject.CompareTag("zemin") && GetComponent<Rigidbody>().velocity.y <=0.01f)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;     
            isGatherable = true;   
        }    

        if(other.gameObject.CompareTag("duvar"))
        {
            Destroy(gameObject);
            //SUYA DÜŞME VE SENİN CAN AZALMA
        }

        if(other.gameObject.TryGetComponent<TrashCan>(out TrashCan tc))
        {
            if(((int)tc.TCType) == ((int)tt))
            {
                Destroy(gameObject);
                //DÜŞMAN CAN AZALTMA
            }
            else
            {
                //SENİN CANIN AZALSIN
            }
        }
    }

    void OnCollisionStay(Collision other)
    {
        waitTime+= Time.deltaTime;
        if(canMakePoisonousArea&& waitTime > maxWaitTime)
        {
            canMakePoisonousArea = false;
            waitTime = 0;
            Instantiate(poisonousArea,  new Vector3(transform.position.x, 0.01f, transform.position.z) ,poisonousArea.transform.rotation);
            Destroy(gameObject);
        }
    }
}
