using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody rb;
    private Vector2 playerDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(directionX,directionY).normalized;
    }

    void FixedUpdate() 
    {
        rb.velocity = new Vector3(playerDirection.x * playerSpeed , 0,playerDirection.y * playerSpeed);
        //Debug.Log(rb.velocity);
    }
}
