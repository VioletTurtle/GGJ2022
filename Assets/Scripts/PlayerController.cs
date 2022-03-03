using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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
    public float Oil = 100f;
    public bool oilEmpty = false;

    public bool lampOn;
    private Light2D light2d;
    private LanternRaycast lantray;

    public AnimationScript animScript;

    private GameObject currentPlatform;
    private Vector3 platformOffset;
    private setCursor cursorScript;

    private AudioSource aSource;
    public AudioClip soundJump;
    public AudioClip soundOpen;
    public AudioClip soundClose;

    //not an elegant solution
    //0.1 seconds to give fixed update a chance to update ground state to prvent super jump by having multiple jump inputs in one fixed update framea
    private float jumpDelayTime;

    private MenuImageChanger winChecker;
    private FlashlightAiming aiming;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        light2d = gameObject.transform.GetChild(0).GetComponentInChildren<Light2D>();
        lantray = GetComponentInChildren<LanternRaycast>();
        cursorScript = GetComponent<setCursor>();
        aSource = GetComponent<AudioSource>();
        
        lampOn = true;
        ToggleLight(false);

        winChecker = GameObject.FindGameObjectWithTag("WinDetect").GetComponent<MenuImageChanger>();
        aiming = GetComponentInChildren<FlashlightAiming>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Keypad0)) //space, right click, or num zero for jump
            Jump();
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            ToggleLight(!lampOn);
        
        AirborneMath();

        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();

        if(!oilEmpty && lampOn)
        {
            DrainOil(); //Drain oil if lamp is on
            if (Oil <= 0)
            {
                oilEmpty = true;
                ToggleLight(false);
            }
        }

    }

    private void ToggleLight(bool on)
    {
        if (!lampOn && !isFalling && on && !oilEmpty)
        {
            lampOn = true;
            light2d.enabled = lampOn;
            lantray.enabled = lampOn;
            animScript.UpdateLanternSprite(lampOn);
            cursorScript.UpdateCursor(setCursor.CursorType.reticleOn);
            aSource.clip = soundOpen;
            aSource.Play();
        }
        if(lampOn && !on)
        {
            lampOn = false;
            light2d.enabled = lampOn;
            lantray.enabled = lampOn;
            animScript.UpdateLanternSprite(lampOn);
            cursorScript.UpdateCursor(setCursor.CursorType.reticleOff); 
            aSource.clip = soundClose;
            aSource.Play();
        }
    }

    public void TurnLightOff()
    {
        ToggleLight(false);
    }
    void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * Speed;
        rigBody.velocity = new Vector2((rigBody.velocity.x + moveX) * friction, rigBody.velocity.y); //adding fake friction
    }

    void Jump()
    {
        if (isGrounded && jumpDelayTime >= 0.1f)
        {
            aSource.clip = soundJump;
            aSource.Play();
            isGrounded = false;
            jumpDelayTime = 0f;
            //Debug.Log("jumped!");
            //V = Square root of (2 g h)
            float jumpImpulse = Mathf.Sqrt(2 * rigBody.gravityScale * jumpHeight);
            rigBody.AddForce(transform.up * jumpImpulse, ForceMode2D.Impulse);
            //rigBody.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
            //startingTime = Time.time; //Replace with Time.deltaTime;
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
    private void FixedUpdate()
    {
        isGrounded = false;
        currentPlatform = null;
        gameObject.transform.parent = null;
        animScript.UpdateJump(!isGrounded); //call whenever isgrounded is updated

        if (jumpDelayTime < 0.1f)
            jumpDelayTime += Time.fixedDeltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CheckGround(collision.contacts, collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CheckGround(collision.contacts, collision);
        }
    }

    public void EnemyAttack(Vector2 dir)
    {
        dir.y = 5;

        rigBody.AddForce(dir, ForceMode2D.Impulse);
        TurnLightOff();
        isFalling = true;
        cursorScript.UpdateCursor(setCursor.CursorType.reticleDisabled);
    }

    private void CheckGround(ContactPoint2D[] contacts, Collision2D collision)
    {
        isGrounded = false;
        foreach (ContactPoint2D cp in contacts)
        {
            //Debug.Log(cp.normal);
            Debug.DrawRay(cp.point, cp.normal);
            if (cp.normal.y >= 0.9) //prevent thinking walls are ground
            {
                Debug.DrawRay(cp.point, cp.normal, Color.green);
                isGrounded = true;
            }
        }

        if (isGrounded)
        {
            timeInAir = 0;
            isFalling = false;
            if (!lampOn) //change the reticle from X to off
            {
                cursorScript.UpdateCursor(setCursor.CursorType.reticleOff);
            }
            animScript.UpdateJump(!isGrounded); //call whenever isgrounded is updated

            if (currentPlatform == null && collision.gameObject.GetComponentInChildren<PlatformMovement>() != null)
            {
                currentPlatform = collision.gameObject.GetComponentInChildren<PlatformMovement>().gameObject;
                //Debug.LogWarning("setting parent to platform");
                gameObject.transform.SetParent(currentPlatform.transform, true); //WARNING this gets set every physics frame but whatever
                //platformOffset = currentPlatform.transform.position - transform.position;
            }
        }
    }

    void TogglePause()
    {
        if(winChecker.hasWon == false) //dont pause during exit fade
        {
            Time.timeScale = paused ? 1f : 0f;
            aiming.enabled = paused;
            paused = !paused;
        }
    }

    void DrainOil()
    {
        if (lampOn)
        {
            Oil -= 2 * Time.deltaTime;
            if (Oil < 0)
            {
                ToggleLight(false);
                oilEmpty = true;
                Oil = 0;
            }
            
        }
    }

    public void ChangeOil(float amount)
    {
        Oil += amount * 2 * Time.deltaTime;
        if(Oil > 0)
        {
            oilEmpty = false;
        }
        if(Oil > 100)
        {
            Oil = 100;
        }
    }

    public void SetOil(float amount)
    {
        Oil = amount;
    }

}
