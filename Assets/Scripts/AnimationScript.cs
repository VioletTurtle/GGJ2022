using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    //---Body---
    public SpriteRenderer openSprite;
    public SpriteRenderer closeSprite;
    public SpriteRenderer backSprite;

    public Transform lanternTransform;

    public Sprite[] OpenSprites;
    public Sprite[] ClosedSprites;
    public Sprite[] BackSprites;

    float angle;
    int spriteIndex;
    bool lanternOpen;

    //---Legs---
    public Animator legAnims;
    Rigidbody2D body;
    bool right;


    //---Eyes---
    public SpriteRenderer eyesSprite;
    public Sprite[] EyesSprites1;
    public Sprite[] EyesSprites2;
    Vector3 eyesStartPos;
    Vector3 refVector = Vector3.zero;
    public float eyesSmoothTime = 0.05f;
    public float eyesDistanceFactor = 25f;

    public Sprite blinkSprite1;
    public Sprite blinkSprite2;

    public float fuzzInterval = 0.5f;
    bool fuzz;
    public float blinkInterval = 3f;
    bool blink;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponentInParent<Rigidbody2D>();

        //disable closed sprite on start for now (to be updated by UpdateLanternSprite
        if(openSprite.enabled)
            closeSprite.enabled = false;

        eyesStartPos = eyesSprite.transform.localPosition;

        StartCoroutine(Coro_FuzzToggle());

        StartCoroutine(Coro_Blink(blinkInterval + Random.Range(-0.5f, 1f))); //random blinks
    }

    // Update is called once per frame
    void Update()
    {
        //--------------Body Animations---------------------

        angle = lanternTransform.transform.eulerAngles.z;

        //correcting angle so that left is 0 and right is 180, and it goes from 0 to 360
        angle += 180;
        if (angle > 360f)
        {
            angle -= 360;
        }
        //remap the angle to a number in the index
        spriteIndex = Mathf.RoundToInt(Mathf.Lerp(0, 47, angle / 360));

        //Debug.Log("angle: " + angle + "; index: " + spriteIndex);

        openSprite.sprite = OpenSprites[spriteIndex];
        closeSprite.sprite = ClosedSprites[spriteIndex];
        backSprite.sprite = BackSprites[spriteIndex];

        //--------------Leg Animations---------------------
        if(body.velocity.sqrMagnitude > 0.1f)
        {
            right = body.velocity.x > 0 ? true : false;
        }
        legAnims.SetBool("Right", right);

        legAnims.SetBool("Run", body.velocity.sqrMagnitude > 0.1f);

        //Debug.Log(body.velocity + "; right: " + right + "; Run: " + (body.velocity.sqrMagnitude > 0.1f));


        //--------------Eyes Animations---------------------

        //remap the angle to a number in the index
        spriteIndex = Mathf.RoundToInt(Mathf.Lerp(0, 7, angle / 360));

        if (blink || !lanternOpen)
        {
            eyesSprite.sprite = fuzz ? blinkSprite1 : blinkSprite2;
        }
        else
        {

            eyesSprite.sprite = fuzz ? EyesSprites1[spriteIndex] : EyesSprites2[spriteIndex];
        }

        Vector3 velocity3d = new Vector3(body.velocity.x, body.velocity.y, 0f);
        velocity3d = velocity3d.sqrMagnitude < 0.1f ? Vector3.zero : velocity3d;
        Vector3 targetPos = eyesStartPos - velocity3d / eyesDistanceFactor;
        //eyesSprite.transform.localPosition = (-velocity3d + eyesStartPos) / 100f;
        //targetPos = Vector3.Lerp(eyesStartPos, targetPos, Time.deltaTime);
        eyesSprite.transform.localPosition = Vector3.SmoothDamp(eyesSprite.transform.localPosition, targetPos, ref refVector, eyesSmoothTime);
        
        //Debug.Log("eyePos: " + eyesSprite.transform.localPosition + "; targetpos: " + targetPos);

    }

    public void UpdateLanternSprite(bool on)
    {
        lanternOpen = on;
        if (lanternOpen)
        {
            openSprite.enabled = true;
            closeSprite.enabled = false;
        }
        else
        {
            openSprite.enabled = false;
            closeSprite.enabled = true;
        }
    }

    public void UpdateJump(bool jumping)
    {
        legAnims.SetBool("Jump", jumping);
    }


    IEnumerator Coro_FuzzToggle()
    {
        yield return new WaitForSeconds(fuzzInterval);
        fuzz = !fuzz;

        StartCoroutine(Coro_FuzzToggle());
    }
    IEnumerator Coro_Blink(float time)
    {
        yield return new WaitForSeconds(time);
        blink = true;

        StartCoroutine(Coro_UnBlink());
    }
    IEnumerator Coro_UnBlink()
    {
        yield return new WaitForSeconds(0.25f);
        blink = false;

        StartCoroutine(Coro_Blink(blinkInterval + Random.Range(-0.5f, 1f))); //random blinks
    }
}
