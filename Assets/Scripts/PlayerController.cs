using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigBody;
    public float Speed = 5;
    public float friction = 0.2f;
    //public float jumpSpeed = 300f;
    public float jumpHeight = 1f;
    public bool isGrounded = false;
    [HideInInspector]
    public bool isFalling = false;
    float startingTime;
    public float fallTimer = .3f;
    private float timeInAir = 0f;

    public GameObject lantern;
    public AnimationScript animScript;
 
    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        lantern = gameObject.transform.GetChild(0).gameObject;
        animScript.UpdateLanternSprite(lantern.activeSelf);
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
            animScript.UpdateLanternSprite(lantern.activeSelf);
        }
    }

    public void TurnLightOff()
    {
        if (lantern.activeSelf)
        {
            lantern.SetActive(false);
        }

        Debug.LogWarning("Updating lantern on/off: " + lantern.activeSelf);
        animScript.UpdateLanternSprite(lantern.activeSelf);
    }
    void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * Speed;
        if (isGrounded) //apply fake friction
            rigBody.velocity = new Vector2(rigBody.velocity.x * friction, rigBody.velocity.y);;

        rigBody.velocity = new Vector2(moveX, rigBody.velocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //V = Square root of (2 g h)
                float jumpImpulse = Mathf.Sqrt(2 * rigBody.gravityScale * jumpHeight);
                rigBody.AddForce(transform.up * jumpImpulse, ForceMode2D.Impulse);
                //rigBody.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
                isGrounded = false;
                //startingTime = Time.time; //Replace with Time.deltaTime;

            }
        }
    }

    private void AirborneMath()
    {
        if (!isGrounded)
        {
            if(rigBody.velocity.y < 0f) //only start counting down air timer when moving down
                timeInAir += Time.deltaTime; //replace from Time.time - starting

            //Debug.Log("time in air: " + timeInAir);

            if (timeInAir >= fallTimer) 
            {
                isFalling = true;

                TurnLightOff();
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false; 
            animScript.UpdateJump(!isGrounded); //call whenever isgrounded is updated
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 dir = collision.gameObject.transform.position - gameObject.transform.position;
            //Debug.Log(dir.y);
            if(dir.y <= 0)
            {
                isGrounded = true;
                timeInAir = 0;
                isFalling = false; 
                animScript.UpdateJump(!isGrounded); //call whenever isgrounded is updated
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
