using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothScript : EnemyScript
{
    private MothAnims mothAnimate;
    public int waypointIndex = 0;
    public List<Transform> waypoints;
    // Start is called before the first frame update

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mothAnimate = GetComponentInChildren<MothAnims>();
    }

    public override void InLight()
    {
        //While in light the moth will pursue the player
        Transform target = player.transform;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        mothAnimate.right = (target.position - transform.position).normalized.x > 0 ? true : false;
    }

    public override void NotInLight()
    {
        //While not in light the moth will patrol
        if (waypointIndex < waypoints.Count)
        {
            Debug.Log(waypointIndex);
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            //if(transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
          
            if ((transform.position - targetPosition).sqrMagnitude < 0.8f)
            {
                if (waypointIndex + 1 >= waypoints.Count)
                    waypointIndex = 0;
                else
                    waypointIndex++;
            }
            mothAnimate.right = (targetPosition - transform.position).normalized.x > 0 ? true : false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")//If hit by enemy tagged object, knockback
        {
            Vector2 dir = collision.gameObject.transform.position - gameObject.transform.position;
            collision.gameObject.GetComponent<PlayerController>().EnemyAttack(dir);
        }
    }


}
