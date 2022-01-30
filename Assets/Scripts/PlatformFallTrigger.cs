using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallTrigger : MonoBehaviour
{
    public bool colliding;

    //PlatformBasicController platcontrol;
    //private void Start()
    //{
    //    platcontrol = GetComponentInParent<PlatformBasicController>();
    //}
    private void FixedUpdate()
    {
        //colliding = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            colliding = true;
            //Debug.LogWarning("Tried to turn off");
            //other.gameObject.GetComponent<PlayerController>().TurnLightOff();
            //other.gameObject.GetComponent<PlayerController>().isFalling = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            colliding = true;
            //Debug.LogWarning("Tried to turn off");
            //other.gameObject.GetComponent<PlayerController>().TurnLightOff();
            //other.gameObject.GetComponent<PlayerController>().isFalling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            colliding = false;
        }

    }
}
