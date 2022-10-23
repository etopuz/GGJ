using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    public float maxSinir = 7f;
    [SerializeField] float trashMagnetSpeed = 10f;
    [SerializeField] float closeEnough = 1f;
    [SerializeField] float health = 100f;
    [SerializeField] Image holdedTrashImage;
    [SerializeField] GameManager gameManager;

    Animator anim;

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
        anim = GetComponent<Animator>();
        gameManager = GameManager.instance;
        gameManager.OnStartGame += SetHealthFull;
    }

    void Update()
    {
        if (health <= 0)
        {
            gameManager.state = GameState.Failed;
            Debug.Log("SEN ÖLDÜN");
        }
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        anim.SetFloat("moveX", directionX);
        if(directionX <0 ){
            anim.SetBool("mirror",true);
        }
        else{
            anim.SetBool("mirror", false);
        }
        anim.SetFloat("moveY", directionY);
        playerDirection = new Vector2(directionX,directionY);

        if(collectedThrashes.TryPeek(out GameObject gObj))
        {
            var col = holdedTrashImage.color;
            holdedTrashImage.color = new Color(col.r,col.g,col.b,1);
            holdedTrashImage.sprite = gObj.GetComponent<SpriteRenderer>().sprite;
            holdedTrashImage.SetNativeSize();

            var size = holdedTrashImage.rectTransform.sizeDelta;
            float smallFactor = 0;
            if (size.x > size.y)
            {
                smallFactor = size.x / 50; //width of recttrasnform
            }
            else
            {
                smallFactor = size.y / 50;
            }

            holdedTrashImage.rectTransform.sizeDelta /= smallFactor;
        }
        else
        {
            var col = holdedTrashImage.color;
            holdedTrashImage.color = new Color(col.r, col.g, col.b, 0);
        }


    }

    void FixedUpdate() 
    {
        var pos = transform.position;
        if(pos.x > maxSinir && playerDirection.x>0)
        {
            playerDirection -= new Vector2(playerDirection.x,0);
        }
        if (pos.x < -maxSinir && playerDirection.x < 0)
        {
            playerDirection -= new Vector2(playerDirection.x, 0);
        }
        if (pos.z > maxSinir && playerDirection.y > 0)
        {
            playerDirection -= new Vector2(0,playerDirection.y);
        }
        if (pos.z < -maxSinir && playerDirection.y < 0)
        {
            playerDirection -= new Vector2(0, playerDirection.y);
        }


        playerDirection = playerDirection.normalized;
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


    private void SetHealthFull()
    {
        health = 100;
    }
}
