using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAnims : MonoBehaviour
{
    public bool right = false;
    public float noiseStrength;
    public float noiseFrequency;
    public float LerpSpeed;
    Animator mothAnim;
    GameObject spriteObj;

    // Start is called before the first frame update
    void Start()
    {
        mothAnim = GetComponentInChildren<Animator>();
        spriteObj = mothAnim.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(right)
        {
            mothAnim.SetBool("Right", true);
        }
        else
        {
            mothAnim.SetBool("Right", false);
        }


        float PosY = noiseStrength * Mathf.Sin(Random.Range(-1f, 1f) * noiseFrequency);
        float PosX = noiseStrength * Mathf.Sin(Random.Range(-1f, 1f) * noiseFrequency);

        spriteObj.transform.localPosition = Vector2.Lerp(spriteObj.transform.localPosition, new Vector2(PosX, PosY), Time.deltaTime * LerpSpeed);
    }
}
