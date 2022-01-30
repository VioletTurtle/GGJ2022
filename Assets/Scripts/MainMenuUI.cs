using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject UnlitBack;
    public GameObject LitBack;
    public void PlayGame()
    {
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
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
