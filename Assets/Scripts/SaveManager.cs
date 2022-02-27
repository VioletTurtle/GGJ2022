using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    XmlDocument playerValues = new XmlDocument();
    string filepath = Application.streamingAssetsPath + "/" + "savefile.xml";
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        float xPos = 0.0f;
        float yPos = 0.0f;
        float zPos = 0.0f;
        float stamina = 0.0f;
        if(player != null)
        {
            try { playerValues.Load(filepath); }
            catch (System.IO.FileNotFoundException)
            {
               Debug.Log("Couldn't find file");
            }
            XmlNode root = playerValues.FirstChild;
            foreach(XmlNode node in root.ChildNodes)
            {
                switch (node.Name)
                {
                    case "xPos":
                        xPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "yPos":
                        yPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "zPos":
                        zPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "stamina":
                        stamina = Convert.ToSingle(node.InnerText);
                        break;
                }
            }
            player.transform.position = new Vector3(xPos, yPos, zPos);
            player.GetComponent<PlayerController>().SetOil(stamina);
        }
        //Load Game on start of scene
    }

    private void OnApplicationQuit()
    {
        if(player != null)
        {
            try { playerValues.Load(filepath); }
            catch (System.IO.FileNotFoundException)
            {
                Debug.Log("Couldn't find file");
            }

            XmlNode root = playerValues.FirstChild;

            foreach(XmlNode node in root.ChildNodes)
            {
                switch (node.Name)
                {
                    case "xPos":
                        node.InnerText = player.transform.position.x.ToString();
                        break;
                    case "yPos":
                        node.InnerText = player.transform.position.y.ToString();
                        break;
                    case "zPos":
                        node.InnerText = player.transform.position.z.ToString();
                        break;
                    case "stamina":
                        node.InnerText = player.GetComponent<PlayerController>().Oil.ToString();
                        break;
                }
            }

            playerValues.Save(filepath);
        }
        //Save Game
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TestSave();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            TestLoad();
        }
    }

    void TestSave()
    {
        
        if (player != null)
        {
            try { playerValues.Load(filepath); }
            catch (System.IO.FileNotFoundException)
            {
                Debug.Log("Couldn't find file");
            }

            XmlNode root = playerValues.FirstChild;

            foreach (XmlNode node in root.ChildNodes)
            {
                switch (node.Name)
                {
                    case "xPos":
                        node.InnerText = player.transform.position.x.ToString();
                        break;
                    case "yPos":
                        node.InnerText = player.transform.position.y.ToString();
                        break;
                    case "zPos":
                        node.InnerText = player.transform.position.z.ToString();
                        break;
                    case "stamina":
                        node.InnerText = player.GetComponent<PlayerController>().Oil.ToString();
                        break;
                }
            }

            playerValues.Save(filepath);
        }
    }

    void TestLoad()
    {
        float xPos = 0.0f;
        float yPos = 0.0f;
        float zPos = 0.0f;
        float stamina = 0.0f;
        if (player != null)
        {
            try { playerValues.Load(filepath); }
            catch (System.IO.FileNotFoundException)
            {
                Debug.Log("Couldn't find file");
            }
            XmlNode root = playerValues.FirstChild;
            foreach (XmlNode node in root.ChildNodes)
            {
                switch (node.Name)
                {
                    case "xPos":
                        xPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "yPos":
                        yPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "zPos":
                        zPos = Convert.ToSingle(node.InnerText);
                        break;
                    case "stamina":
                        stamina = Convert.ToSingle(node.InnerText);
                        break;
                }
            }
            player.transform.position = new Vector3(xPos, yPos, zPos);
            player.GetComponent<PlayerController>().SetOil(stamina);
        }
    }
}
