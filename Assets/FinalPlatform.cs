using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    bool slowTimeCheck = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SlowTime();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale += -0.5f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //steadily slow time for a certain amount of time. 
            slowTimeCheck = true;
            
        }
    }

    void SlowTime()
    {
        if (slowTimeCheck) {
            Time.timeScale -= Time.deltaTime * 3;
            if (Time.timeScale <= 0.25)
            {
                slowTimeCheck = false;
                Time.timeScale = 1.0f;
            }
        }
    }
}
