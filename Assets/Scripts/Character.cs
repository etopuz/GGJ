using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float trashMagnetSpeed = 10f;
    [SerializeField] float closeEnough = 1f;
    [SerializeField] float health = 100f;

    public float Health
    {
        get => health;
        set => health = value;
    }

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
        if (health <= 0)
        {
            Debug.Log("SEN ÖLDÜN");
        }
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
        if (!other.TryGetComponent<Trash>(out Trash t) || !t.IsGatherable)
        {
            return;
        }
        
        other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, trashMagnetSpeed * Time.deltaTime);

        if((transform.position - other.transform.position).sqrMagnitude < closeEnough)
        {
            other.GetComponent<Trash>().IsGatherable = false;
            other.GetComponent<Trash>().IsStopable = false;
            collectedThrashes.Push(other.gameObject);
            other.gameObject.SetActive(false);  
        }        
    }
}
