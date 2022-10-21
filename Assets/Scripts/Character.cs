using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float trashMagnetSpeed = 10f;
    [SerializeField] float closeEnough = 1f;

    private Stack<GameObject> collectedThrashes = new Stack<GameObject>();
    private Rigidbody rb;
    private Vector2 playerDirection;

    public Stack<GameObject> CollectedThrashes
    {
        get => collectedThrashes;
        set => collectedThrashes = value;
    }

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
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent<Collectable>(out Collectable c))
        {
            return;
        }
        
        other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, trashMagnetSpeed * Time.deltaTime);

        if((transform.position - other.transform.position).sqrMagnitude < closeEnough)
        {
            collectedThrashes.Push(other.gameObject);
            Destroy(other.gameObject);
        }        
    }
}
