using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCursor : MonoBehaviour
{
    public Texture2D crosshair_main1;
    public Texture2D crosshair_main2;
    public Texture2D crosshair_on;
    public Texture2D crosshair_off;
    public Texture2D crosshair_disabled;

    public enum CursorType { arrow1, arrow2, reticleOn, reticleOff, reticleDisabled };
    public CursorType startType = CursorType.arrow1;
    private CursorType currentType = CursorType.arrow1;

    Vector2 cursorOffset;

    float blinkInterval = 3f;
    bool blink = false;

    void Start()
    {
        //set the cursor origin to its centre. (default is upper left corner)
        cursorOffset = new Vector2(crosshair_on.width / 2, crosshair_on.height / 2);

        UpdateCursor(startType);

        StartCoroutine(Coro_Blink(blinkInterval + Random.Range(-0.5f, 1f))); //random blinks
    }

    public void UpdateCursor(CursorType newtype)
    {
        if(currentType != newtype)
        {
            currentType = newtype;

            switch (currentType)
            {
                case CursorType.arrow1: //main eyes
                    Cursor.SetCursor(crosshair_main1, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorType.arrow2: //main blink
                    Cursor.SetCursor(crosshair_main2, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorType.reticleOn: //on
                    Cursor.SetCursor(crosshair_on, cursorOffset, CursorMode.Auto);
                    blink = false;
                    break;
                case CursorType.reticleOff: //off
                    Cursor.SetCursor(crosshair_off, cursorOffset, CursorMode.Auto);
                    blink = false;
                    break;
                case CursorType.reticleDisabled: //off
                    Cursor.SetCursor(crosshair_disabled, cursorOffset, CursorMode.Auto);
                    blink = false;
                    break;
                default:
                    break;
            }
        }
    }

    private void Update()
    {
        if(currentType == CursorType.arrow1 || currentType == CursorType.arrow2)
        {
            if (blink)
            {
                UpdateCursor(CursorType.arrow2);
            }
            else
            {
                UpdateCursor(CursorType.arrow1);
            }
        }
    }

    IEnumerator Coro_Blink(float time)
    {
        yield return new WaitForSeconds(time);
        blink = true;

        StartCoroutine(Coro_UnBlink());
    }
    IEnumerator Coro_UnBlink()
    {
        yield return new WaitForSeconds(0.25f);
        blink = false;

        StartCoroutine(Coro_Blink(blinkInterval + Random.Range(-0.5f, 1f))); //random blinks
    }
}
