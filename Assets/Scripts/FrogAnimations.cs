using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimations : MonoBehaviour
{
    public SpriteRenderer frogSpriteSleep;
    public SpriteRenderer frogSpriteAwake;

    public Animator sleepAnimator;

    public Sprite[] awakeSprites = new Sprite[4];

    public bool sleeping;
    int spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        sleepAnimator.speed = 0.05f;
        UpdateSleep(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sleeping)
        {
            frogSpriteAwake.sprite = awakeSprites[spriteIndex];
        }
    }

    public void UpdateSpriteIndex(float frogTime, float maxTime)
    {
        spriteIndex = Mathf.RoundToInt(Mathf.Lerp(0, 3, frogTime / maxTime));
    }

    public void UpdateSleep(bool sleep)
    {
        sleeping = sleep;

        frogSpriteAwake.enabled = !sleep;
        frogSpriteSleep.enabled = sleep;
    }
}
