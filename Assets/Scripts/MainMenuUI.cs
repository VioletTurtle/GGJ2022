using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject UnlitBack;
    public GameObject LitBack;
    public AudioMixer audioMixer;
    public string MixerName;
    public void PlayGame()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(MixerName, volume);
    }

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("WinDetect").GetComponent<MenuImageChanger>().hasWon == false)
        {
            UnlitBack.SetActive(true);
            LitBack.SetActive(false);
        }
        else if (GameObject.FindGameObjectWithTag("WinDetect").GetComponent<MenuImageChanger>().hasWon == true)
        {
            UnlitBack.SetActive(false);
            LitBack.SetActive(true);
        }
    }
}
