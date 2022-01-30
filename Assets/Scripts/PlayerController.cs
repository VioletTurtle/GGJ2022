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
    float startingTime;
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

    public void TurnLightOff()
    {
        if (lantern.activeSelf)
        {
            lantern.SetActive(false);
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
                startingTime = Time.time;

            }
        }
    }

    private void AirborneMath()
    {
        if (!isGrounded)
        {
            timeInAir += Time.time - startingTime;
            if (timeInAir >= fallTimer)
            {
                isFalling = true;
                
                lantern.SetActive(false);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 dir = collision.gameObject.transform.position - gameObject.transform.position;
            Debug.Log(dir.y);
            if(dir.y <= 0)
            {
                isGrounded = true;
                timeInAir = 0;
                isFalling = false;
            }
        }

        

        if(collision.gameObject.tag == "Enemy")//If hit by enemy tagged object, knockback
        {
            if (collision.gameObject.GetComponent<EnemyController>().aiType == EnemyType.Moth) ;
            {
                Vector2 dir = gameObject.transform.position - collision.gameObject.transform.position;
                //Right side will be positive, left side will be negative
                EnemyAttack(dir);
            }
            
        }
    }

    public void EnemyAttack(Vector2 dir)
    {
        

        dir.y = 5;

        rigBody.AddForce(dir, ForceMode2D.Impulse);
        TurnLightOff();
    }
}
