using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{
    public GameObject blackBox;
    public Image blackFade;
    bool readyToFade = false;
    // Start is called before the first frame update
    void Awake()
    {
        blackFade.gameObject.SetActive(true);
        var tempColor = blackFade.GetComponent<Image>().color;
        tempColor.a = 1;
        blackFade.color = tempColor;
        readyToFade = true;
    }

    private void Update()
    {
        if (readyToFade)
        {
            var tempColor = blackFade.GetComponent<Image>().color;
            tempColor.a += Time.deltaTime * -0.5f;
            blackFade.color = tempColor;
            if(tempColor.a == 0)
            {
                blackBox.gameObject.SetActive(false);
            }
        }
    }


}
