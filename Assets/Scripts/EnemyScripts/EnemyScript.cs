using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBehaviors {inLight, notInLight}
public class EnemyScript : MonoBehaviour
{
    public EBehaviors eBehaviors = EBehaviors.notInLight;
    public Transform player;
    public float speed;
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        RunBehavior();
        ToggleOff();
    }

    public virtual void RunBehavior()
    {
        switch (eBehaviors)
        {
            case EBehaviors.notInLight:
                NotInLight();
                break;
            case EBehaviors.inLight:
                InLight();
                break;
        }
    }


    public virtual void ReactToLight()
    {
        eBehaviors = EBehaviors.inLight;
    }
    
    public virtual void InLight()
    {

    }

    public virtual void NotInLight()
    {

    }


    public virtual void ToggleOff()
    {
        eBehaviors = EBehaviors.notInLight;
    }
}
