using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBasicController : MonoBehaviour
{
    private BoxCollider2D col;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnOff();
    }

    private void TurnOff()
    {
        col.enabled = false;
        sr.enabled = false;
    }

    public void TurnOn()
    {
        col.enabled = true;
        sr.enabled = true;
    }
}
