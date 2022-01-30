using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    public Light2D light2d;
    public float frequency;
    public float maxsize;
    private float startsize;

    // Start is called before the first frame update
    void Start()
    {
        startsize = light2d.pointLightInnerRadius;
    }

    // Update is called once per frame
    void Update()
    {
        float size = Mathf.Lerp(startsize, startsize * maxsize, (Mathf.Sin(Time.time * frequency) + 1)/2);
        light2d.pointLightOuterRadius = size;
        light2d.pointLightInnerRadius = size;
    }
}
