using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private GameObject player;
    private float playerHeight;

    AudioSource[] audioSources = new AudioSource[7];
    public float thirdLayerHeight = 20f;
    public float fourthLayerHeight = 40f;
    public float fifthLayerHeight = 60f;
    public float sixthLayerHeight = 80f;
    public float seventhLayerHeight = 1000f;

    void Start()
    {
        player = GameObject.Find("Player");
        playerHeight = player.transform.position.y;

        int index = 0;
        foreach (AudioSource aSource in GetComponents<AudioSource>())
        {
            audioSources.SetValue(aSource, index);
            index++;
        }
        foreach (AudioSource aSource in audioSources)
        {
            aSource.Play();
        }
    }

    
    void Update()
    {
        playerHeight = player.transform.position.y;

        if (playerHeight >= seventhLayerHeight)
        {
            audioSources[6].volume = 1.0f;
        }
        else
        {
            audioSources[6].volume = 0.0f;
        }
        if (playerHeight >= sixthLayerHeight)
        {
            audioSources[5].volume = 1.0f;
        }
        else 
        {
            audioSources[5].volume = 0.0f;
        }
        if (playerHeight >= fifthLayerHeight)
        {
            audioSources[4].volume = 1.0f;
        }
        else
        {
            audioSources[4].volume = 0.0f;
        }
        if (playerHeight >= fourthLayerHeight)
        {
            audioSources[3].volume = 1.0f;
        }
        else
        {
            audioSources[3].volume = 0.0f;
        }
        if (playerHeight >= thirdLayerHeight)
        {
            audioSources[2].volume = 1.0f;
        }
        else
        {
            audioSources[2].volume = 0.0f;
        }
    }
}
