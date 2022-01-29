using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behaviors {Patrol, Attack, Idle};
public enum EnemyType { Moth, Frog};

public class EnemyController : MonoBehaviour
{
    
    public Behaviors aiBehavior = Behaviors.Patrol;
    public EnemyType aiType = EnemyType.Moth;
    public Transform player;
    public float speed;
    int waypointIndex = 0;
    float frogAttackTimer = 1f;
    public List<Transform> waypoints;
    bool attackReady = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        runBehavior();
        ToggleOff();
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
            case Behaviors.Idle:
                Idle();
                break;
        }
    }

    void ToggleOff()
    {
        if(aiType == EnemyType.Moth)
        {
            aiBehavior = Behaviors.Patrol;
        }
        if(aiType == EnemyType.Frog)
        {
            aiBehavior = Behaviors.Idle;
        }
    }
    void Move()
    {

    }
    void Idle()
    {
        if(aiType == EnemyType.Moth)
        {
            aiBehavior = Behaviors.Patrol;
        }
    }
    void Attack()
    {
        if (aiType == EnemyType.Moth)
        {
            //get light source
            //Move moth toward light source
            Transform target = player.transform;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }
        if (aiType == EnemyType.Frog)
        {
            if (attackReady == true)
            {
                attackReady = false;
                Invoke("FrogAttack", 2);
            }
            //get light source
            //run a countdown/coroutine, if it finishes the frog will attack the light source/player with its tongue

        }
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

    public void ReactToLight()
    {
        aiBehavior = Behaviors.Attack;
        
    }

    void FrogAttack()
    {
        Debug.Log("Frog go blep");
        attackReady = true;
    }

   //timer += time, if it reaches its threshold reset it to 0, otherwise keep incrementing
}
