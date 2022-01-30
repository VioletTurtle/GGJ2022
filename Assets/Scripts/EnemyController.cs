using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behaviors {Patrol, Attack, Idle};
public enum EnemyType { Moth, Frog};

public class EnemyController : MonoBehaviour
{
    
    public Behaviors aiBehavior = Behaviors.Patrol;
    public EnemyType aiType = EnemyType.Moth;
    private Transform player;
    public float speed;
    public int waypointIndex = 0;
    float frogAttackTimer = 2f;
    public List<Transform> waypoints;
    bool attackReady = true;
    GameObject tongueTip;
    public LineRenderer lr;
    float timer;
    public float FrogMaxTime = 2f;

    private MothAnims mothAnimate;
    private FrogAnimations frogAnimate;

    private AudioSource aSource;
    public AudioClip tongueOut;
    public AudioClip tongueIn;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        if (GameObject.Find("Player"))
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
        }
        lr = gameObject.GetComponent<LineRenderer>();

        if (aiType == EnemyType.Moth)
        {
            mothAnimate = GetComponentInChildren<MothAnims>();
        }
        if (aiType == EnemyType.Frog)
        {
            frogAnimate = GetComponentInChildren<FrogAnimations>();
        }
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
        if (aiType == EnemyType.Frog)
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, FrogMaxTime);
            frogAnimate.UpdateSpriteIndex(timer, FrogMaxTime);

            if (timer <= 0)
            {
                if (!frogAnimate.sleeping)
                    frogAnimate.UpdateSleep(true);
            }
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

            mothAnimate.right = (target.position - transform.position).normalized.x > 0 ? true : false;

        }
        if (aiType == EnemyType.Frog)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, FrogMaxTime);
            frogAnimate.UpdateSpriteIndex(timer, FrogMaxTime);

            if(frogAnimate.sleeping)
                frogAnimate.UpdateSleep(false);

            Debug.Log(timer);
            if (timer >= FrogMaxTime)
            {
                if (attackReady == true)
                {
                    Debug.Log("Frog consider blep");
                    attackReady = false;
                    FrogAttack();
                }
            }
            //get light source
            //run a countdown/coroutine, if it finishes the frog will attack the light source/player with its tongue
        }
        
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
                if (waypointIndex + 1 >= waypoints.Count)
                    waypointIndex = 0;
                else
                    waypointIndex++;
                
            }

            mothAnimate.right = (targetPosition - transform.position).normalized.x > 0 ? true : false;
        }
    }

    public void ReactToLight()
    {
        aiBehavior = Behaviors.Attack;
        
        //Check for a boolean value to tell if a timer is already started
        //If one isn't started start a new one and set the bool to false
        //keep track of the time difference, once it hits a certain time difference it will trigger an attack and then
        //reset itself to take a new starting time. 
    }

    void FrogAttack()
    {
        Debug.Log("Frog go blep");

        aSource.clip = tongueOut;
        aSource.Play();

        lr.SetPosition(0, gameObject.transform.position);
        lr.SetPosition(1, player.transform.position);
        attackReady = true;
        StartCoroutine("RemoveTongue");
        player.gameObject.GetComponent<PlayerController>().EnemyAttack(new Vector2(0, 5));
    }

    IEnumerator RemoveTongue()
    {
        lr.enabled = true;
        yield return new WaitForSeconds(0.5f);
        aSource.clip = tongueIn;
        aSource.Play();
        lr.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")//If hit by enemy tagged object, knockback
        {
            Vector2 dir = collision.gameObject.transform.position - gameObject.transform.position;
            collision.gameObject.GetComponent<PlayerController>().EnemyAttack(dir);
        }
    }
    
 
   //timer += time, if it reaches its threshold reset it to 0, otherwise keep incrementing
}
