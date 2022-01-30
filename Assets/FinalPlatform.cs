using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    bool slowTimeCheck = false;
    public GameObject finalLight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SlowTime();
        Debug.Log("Timescale = " + Time.timeScale);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            collision.gameObject.GetComponent<PlayerController>().jumpHeight += 10;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //steadily slow time for a certain amount of time. 
            slowTimeCheck = true;
            //GameObject.FindGameObjectWithTag("WinDetect").GetComponent<MenuImageChanger>().hasWon = true;


        }
    }

    void SlowTime()
    {
        if (slowTimeCheck) {
            Time.timeScale -= Mathf.Pow(Time.deltaTime, 4.0f);
            if (Time.timeScale <= 0.01)
            {
                slowTimeCheck = false;
                Time.timeScale = 1.0f;
            }
        }
    }

    void GrowLight()
    {
        //Steadily grow light range to cover screen
    }
}
