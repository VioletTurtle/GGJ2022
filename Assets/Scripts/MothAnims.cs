using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAnims : MonoBehaviour
{
    Animator mothAnim;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        mothAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(body.velocity.y > 0)
        {
            mothAnim.SetBool("Right", true);
        }
        else
        {
            mothAnim.SetBool("Right", false);
        }

    }
}
