using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    GameObject player;
    Slider slider;
    public Image fill;
    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slider = GetComponent<Slider>();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<PlayerController>().Oil;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
