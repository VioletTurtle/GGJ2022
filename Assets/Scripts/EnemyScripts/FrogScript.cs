using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : EnemyScript
{
    float frogAttackTimer = 2f;
    bool attackReady = true;
    public float tongueSpeed = 0.4f;
    public LineRenderer lr;
    float timer;
    public float FrogMaxTime = 2f;

    private FrogAnimations frogAnimate;

    private AudioSource aSource;
    public AudioClip tongueOut;
    // Start is called before the first frame update
    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aSource = GetComponent<AudioSource>();
        lr = gameObject.GetComponent<LineRenderer>();
        frogAnimate = GetComponentInChildren<FrogAnimations>();
    }

    public override void InLight()
    {
        timer = Mathf.Clamp(timer + Time.deltaTime, 0, FrogMaxTime);
        frogAnimate.UpdateSpriteIndex(timer, FrogMaxTime);

        if (frogAnimate.sleeping)
        {
            frogAnimate.UpdateSleep(false);
        }

        if(timer >= FrogMaxTime)
        {
            if(attackReady == true)
            {
                attackReady = false;
                FrogAttack();
            }
        }
    }

    public override void NotInLight()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, FrogMaxTime);
        frogAnimate.UpdateSpriteIndex(timer, FrogMaxTime);

        if(timer <= 0)
        {
            if (!frogAnimate.sleeping) { frogAnimate.UpdateSleep(true); }
        }
    }

    void FrogAttack()
    {
        aSource.clip = tongueOut;
        aSource.Play();

        lr.SetPosition(0, gameObject.transform.position + new Vector3(0f, .2f, 0f));
        lr.SetPosition(1, gameObject.transform.position);
        lr.enabled = true;
        StartCoroutine("MoveTongue");
        player.gameObject.GetComponent<PlayerController>().EnemyAttack(new Vector2(0, 5));
    }

    IEnumerator MoveTongue()
    {
        while (Vector3.Distance(lr.GetPosition(1), player.transform.position) > .1f)
        {
            lr.SetPosition(1, Vector3.Lerp(lr.GetPosition(1), player.transform.position, tongueSpeed));
            yield return new WaitForSeconds(0.0167f);
        }
        StartCoroutine("RemoveTongue");
    }

    IEnumerator RemoveTongue()
    {
        yield return new WaitForSeconds(0.1f);
        lr.enabled = false;
        attackReady = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 dir = collision.gameObject.transform.position - gameObject.transform.position;
            collision.gameObject.GetComponent<PlayerController>().EnemyAttack(dir);
        }
    }


}
