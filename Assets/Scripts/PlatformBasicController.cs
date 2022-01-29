using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBasicController : MonoBehaviour
{
    private BoxCollider2D collider;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnOff();
    }

    private void TurnOff()
    {
        collider.enabled = false;
        sr.enabled = false;
    }

    public void TurnOn()
    {
        collider.enabled = true;
        sr.enabled = true;
    }
}
