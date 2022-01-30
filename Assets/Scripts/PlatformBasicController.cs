using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBasicController : MonoBehaviour
{
    private float turnoffDelay = 0.5f;
    private float currDelay;
    public bool platformOn = false;
    public bool debug;

    private BoxCollider2D col;
    private SpriteRenderer sr;
    private PlatformFallTrigger falltrig;
    private EyesAnimations eyes;

    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        falltrig = GetComponentInChildren<PlatformFallTrigger>();
        eyes = GetComponentInChildren<EyesAnimations>();

        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (platformOn)
        {
            if (debug)
                Debug.Log(currDelay);

            currDelay -= Time.deltaTime;
            if (currDelay <= 0f)
                TurnOff();
        }
    }

    private void TurnOff()
    {
        platformOn = false;
        col.enabled = false;
        sr.enabled = false;
        eyes.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TurnOn()
    {
        if (!falltrig.colliding) //prevent turning on a platform when the player is colliding with it
        {
            currDelay = turnoffDelay;
            platformOn = true;
            col.enabled = true;
            sr.enabled = true;
            eyes.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}
