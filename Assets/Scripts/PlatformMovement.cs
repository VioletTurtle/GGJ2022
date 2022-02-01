using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType { Stationary, Side2Side, UpNDown};
public class PlatformMovement : MonoBehaviour
{
    public MoveType movementType = MoveType.Stationary;
    public Transform[] horWaypoints;
    public Transform[] verWaypoints;
    [Range(0,10)] public float changeSpeed = 1;
    [Range(0, 5)] public float duration = 1f;

    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        
        //teleport platform to first waypoint because otherwise it lerps there
        switch (movementType)
        {
            case MoveType.Stationary:
                //Nothing
                break;
            case MoveType.Side2Side:
                transform.position = horWaypoints[0].position;
                break;
            case MoveType.UpNDown:
                transform.position = verWaypoints[0].position;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        runBehavior();
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
        var factor = Mathf.PingPong(Time.time / (2f * duration), 1f);
        factor = Mathf.SmoothStep(0, changeSpeed, factor);
        transform.position = Vector2.Lerp(horWaypoints[0].position, horWaypoints[1].position, factor);
        //Translate platform back and forth between two waypoints
        //platform slowing thanks to user276019 on stackoverflow
    }


  

    void UpAndDown()
    {
        var factor = Mathf.PingPong(Time.time / (2f * duration), 1f);
        factor = Mathf.SmoothStep(0, changeSpeed, factor);
        //body.MovePosition( Vector2.Lerp(verWaypoints[0].position, verWaypoints[1].position, factor));
        transform.position = Vector2.Lerp(verWaypoints[0].position, verWaypoints[1].position, factor);
        //Translate platform up and down between two waypoints
        //platform slowing thanks to user276019 on stackoverflow
    }
}
