using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyPlatformScript : MonoBehaviour
{
    GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            player.gameObject.GetComponent<PlayerController>().friction = 0.6f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            StartCoroutine("returnFriction", collision);
        }
    }

    IEnumerator returnFriction(Collision2D collision)
    {
        yield return new WaitForSeconds(0.5f);
        player.gameObject.GetComponent<PlayerController>().friction = 0.4f;
    }
}
