using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveScript : MonoBehaviour
{
    XmlDocument playerValues = new XmlDocument();
    public GameObject player;
    string filePath = Application.streamingAssetsPath + "/" + "savefile.xml";
    void Start()
    {
        float xPos = 0.0f;
        float yPos = 0.0f;
        float zPos = 0.0f;
        float stamina = 0.0f;
        //load
        if(player != null)
        {
            try
            {
                playerValues.Load(filePath);
            }
            catch (IOException)
            {
                Debug.Log("Can't find file");
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
            player.GetComponent<PlayerController>().Oil = stamina;
        }
        else { Debug.Log("Player is null"); }
        
    }

    private void OnApplicationQuit()
    {
        if(player != null)
        {
            try
            {
                playerValues.Load(filePath);
            }
            catch(IOException)
            {
                Debug.Log("Can't find file");
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
            playerValues.Save(filePath);
        }
    }
}
