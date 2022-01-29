using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Tried to turn off");
            other.gameObject.GetComponent<PlayerController>().lantern.SetActive(false);
            other.gameObject.GetComponent<PlayerController>().isFalling = true;
        }
    }
}
