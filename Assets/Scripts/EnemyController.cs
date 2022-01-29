using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behaviors {Patrol, Attack};


public class EnemyController : MonoBehaviour
{
    public Behaviors aiBehavior = Behaviors.Patrol;
    public float speed;
    int waypointIndex = 0;

    public List<Transform> waypoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        runBehavior();
    }

    void runBehavior()
    {
        switch (aiBehavior)
        {
            case Behaviors.Patrol:
                Patrol();
                break;
            case Behaviors.Attack:
                Attack();
                break;
        }
    }

    void Move()
    {

    }

    void Attack()
    {
        //While in light move to towards its source
        //On contact with the player knock them back
    }

    void Patrol()
    {
        //Enemy patrols between waypoints
        //If hit with light change to attack mode
        if(waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
            {
                if (waypointIndex + 1 == waypoints.Count)
                    waypointIndex = 0;
                else
                    waypointIndex++;
                
            }
;        }
    }
}
