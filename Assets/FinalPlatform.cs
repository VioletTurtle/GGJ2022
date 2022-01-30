using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    bool slowTimeCheck = false;
    public GameObject finalLight;
    public Image fadeToWhite;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SlowTime();
        GrowLight();
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
            if (GameObject.FindGameObjectWithTag("WinDetect"))
            {
                GameObject.FindGameObjectWithTag("WinDetect").GetComponent<MenuImageChanger>().hasWon = true;
            }
            
        }
    }

    void SlowTime()
    {
        if (slowTimeCheck) {
            Time.timeScale = Time.timeScale - Time.deltaTime;
            if (Time.timeScale >= 100 || Time.timeScale <= 0.01f)
            {
                slowTimeCheck = false;
                SceneManager.LoadScene("Credits");
            }
            
            
        }
    }

    void GrowLight()
    {
        if (slowTimeCheck)
        {
            var tempColor = fadeToWhite.GetComponent<Image>().color;
            tempColor.a += Time.deltaTime * 1f;
            Debug.Log(tempColor.a);
            fadeToWhite.color = tempColor;
            
        }
        //Steadily grow light range to cover screen
    }
}
