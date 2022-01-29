using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigBody;
    public float Speed = 5;
    public float jumpSpeed = 20f;
    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isFalling = false;

    public float fallTimer = .3f;
    private float timeInAir = 0f;

    public GameObject lantern;
    
 
    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        lantern = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        ToggleLight();
        AirborneMath();
    }

    private void FixedUpdate()
    {
        
    }

    public void ToggleLight()
    {
        if (!isFalling && Input.GetKeyDown(KeyCode.Mouse0))
        {
            lantern.SetActive(!lantern.activeSelf);
        }
    }
    void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float moveBy = hor * Speed;
        rigBody.velocity = new Vector2(moveBy, rigBody.velocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                rigBody.AddForce(transform.up * jumpSpeed);
                isGrounded = false;

            }
        }
    }

    private void AirborneMath()
    {
        if (!isGrounded)
        {
            timeInAir += Time.deltaTime;
            if (timeInAir >= fallTimer)
            {
                isFalling = true;
                lantern.SetActive(false);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isFalling = false;
        }

        if(collision.gameObject.tag == "Enemy")//If hit by enemy tagged object, knockback
        {
            float xMove = 0;
            float yMove = 3;
            Vector2 dir = collision.gameObject.transform.position - gameObject.transform.position;
            //Right side will be positive, left side will be negative
            dir = -dir.normalized;
            dir.y = 5;

            rigBody.AddForce(dir, ForceMode2D.Impulse);
        }
    }
}
