using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float health = 100f;

    public float Health
    {
        get=> health;
        set => health = value;
    }

    private void Update() {
        if(health <= 0)
        {
            Debug.Log("BOSS ÖLDÜÜ");
        }
    }
}
