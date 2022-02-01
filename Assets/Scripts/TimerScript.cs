using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    float startingTime;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        startingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - startingTime;
        text.text = currentTime.ToString("F1");
    }
}
