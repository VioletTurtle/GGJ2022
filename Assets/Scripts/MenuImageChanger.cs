using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuImageChanger : MonoBehaviour
{
    public bool hasWon = false;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("WinDetect");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
