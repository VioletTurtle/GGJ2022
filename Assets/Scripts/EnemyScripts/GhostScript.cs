using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GhostScript : EnemyScript
{

    public override void InLight()
    {
        //Ghost does not move
        //No code needed
    }

    public override void NotInLight()
    {
        //Ghost gets player position
        //Ghost checks Distance
        //if the distance is within its range it will pursue the player when not in the light
        Transform target = player.transform;
        float distance = Vector3.Distance(gameObject.transform.position, target.position);
        float delta = speed * Time.deltaTime;
        if(distance < 30)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, delta);
        }
    }

    
}
