using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType { Stationary, Side2Side, UpNDown};
public class PlatformMovement : MonoBehaviour
{
    public MoveType movementType = MoveType.Stationary;
    public Transform[] horWaypoints;
    public Transform[] verWaypoints;
    int waypointIndex = 0;
    public float platformSpeed = 1;
    [Range(0,10)] public float changeSpeed = 1;
    private float totalTime = 0.0f;
    [Range(0.001f, 2f)]public float frequency = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        runBehavior();
        totalTime += Time.deltaTime;
    }

    void runBehavior()
    {
        switch (movementType)
        {
            case MoveType.Stationary:
                Stationary();
                break;
            case MoveType.Side2Side:
                Side_to_side();
                break;
            case MoveType.UpNDown:
                UpAndDown();
                break;
        }
    }

    void Stationary()
    {
        //Do Nothing
    }


    void Side_to_side()
    {
        
       

        if (waypointIndex < horWaypoints.Length)
        {
            
            //platformSpeed = (-changeSpeed * (float)Mathf.Cos(2f * (float)Mathf.PI * frequency * totalTime));
            Vector3 targetPosition = horWaypoints[waypointIndex].position;
            float delta = platformSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            
            if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
            {
                if (waypointIndex + 1 == horWaypoints.Length)
                    waypointIndex = 0;
                else
                    waypointIndex++;

            }      
            
        }
        else
        {
            waypointIndex = 0;
        }
        //Translate platform back and forth between two waypoints
    }


  

    void UpAndDown()
    {
        if (waypointIndex < verWaypoints.Length)
        {
            platformSpeed = (changeSpeed * (float)Mathf.Cos(2f * (float)Mathf.PI * frequency));
            Vector3 targetPosition = verWaypoints[waypointIndex].position;
            float delta = platformSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
            {
                if (waypointIndex + 1 == verWaypoints.Length)
                    waypointIndex = 0;
                else
                    waypointIndex++;

            }
;
        }
        else
        {
            waypointIndex = 0;
        }
        //Translate platform up and down between two waypoints
    }
}
