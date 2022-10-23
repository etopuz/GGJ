using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float health = 100f;
    GameManager gameManager;

    public void Start()
    {
        gameManager = GameManager.instance;
        gameManager.OnStartGame += SetHealthFull;
    }

    public float Health
    {
        get=> health;
        set => health = value;
    }

    private void Update() {

        if (health <= 0 && gameManager.state == GameState.Playing)
        {
            Debug.Log("BOSS ÖLDÜÜ");
            gameManager.state = GameState.Win;
        }
    }

    private void SetHealthFull()
    {
        health = 100;
    }
}
