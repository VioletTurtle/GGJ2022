using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public Transform bg1;
    public Transform bg2;
    private Vector3 targetPos;

    void FixedUpdate()
    {
        if (player.position.y > 500)
            targetPos = new Vector3(transform.position.x, 100f, transform.position.z);
        else if (player.position.y < -2.5)
            targetPos = new Vector3(transform.position.x, 0f, transform.position.z);
        else
            targetPos = new Vector3(transform.position.x, player.position.y + 2.5f, transform.position.z);
        transform.position = targetPos;
        //transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
        if(transform.position.y >= bg2.position.y)
        {
            bg1.position = new Vector3(bg1.position.x, bg2.position.y + 22f, bg1.position.z);
            SwitchBackground();
        }

        if(transform.position.y < bg1.position.y)
        {
            bg2.position = new Vector3(bg2.position.x, bg1.position.y - 22f, bg2.position.z);
            SwitchBackground();
        }
    }

    private void SwitchBackground()
    {
        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;
    }
}
