using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject PauseMenu;
    public AudioMixer audioMixer;
    public string MixerName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(MixerName, volume);
    }
}
