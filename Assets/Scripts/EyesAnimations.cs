using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesAnimations : MonoBehaviour
{
    float angle;
    int spriteIndex;

    //---Eyes---
    public SpriteRenderer eyesSprite;
    public Sprite[] EyesSprites1;
    public Sprite[] EyesSprites2;
    public Sprite blinkSprite1;
    public Sprite blinkSprite2;

    public float fuzzInterval = 0.5f;
    bool fuzz;
    public float blinkInterval = 3f;
    bool blink;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        StartCoroutine(Coro_FuzzToggle());

        StartCoroutine(Coro_Blink(blinkInterval +  Random.Range(-0.5f, 1f))); //random blinks
    }

    // Update is called once per frame
    void Update()
    {
        //--------------Eyes Animations---------------------
        Vector3 dir = Vector3.Normalize(player.transform.position - transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //angle = Vector3.Angle(player.transform.position - transform.position, transform.right);
        Debug.Log(dir);

        //correcting angle so that left is 0 and right is 180, and it goes from 0 to 360
        angle += 180;
        if (angle > 360f)
        {
            angle -= 360;
        }
        //remap the angle to a number in the index
        spriteIndex = Mathf.RoundToInt(Mathf.Lerp(0, 7, angle / 360));

        if (blink)
        {
            eyesSprite.sprite = fuzz ? blinkSprite1 : blinkSprite2;
        }
        else
        {

            //Debug.Log("angle: " + angle + "; index: " + spriteIndex);
            eyesSprite.sprite = fuzz ? EyesSprites1[spriteIndex] : EyesSprites2[spriteIndex];
        }



        //Debug.Log("eyePos: " + eyesSprite.transform.localPosition + "; targetpos: " + targetPos);
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
