using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisonous : MonoBehaviour
{

    private void OnTriggerStay(Collider other) 
    {
        if(other is SphereCollider){
            return;
        }

        if(other.TryGetComponent<Character>(out Character c))
        {
            Debug.Log(other.gameObject.name);

            c.Health -= 2/60;
        }        
    }    
}
