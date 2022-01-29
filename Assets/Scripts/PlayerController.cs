using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigBody;
    public float Speed = 5;
    public float jumpSpeed = 20f;
    bool isGrounded = false;
 
    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
