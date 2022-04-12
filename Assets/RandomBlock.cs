using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlock : MonoBehaviour
{
    public int blockNum;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        randomSlot();

    }

    public void randomSlot()
    {
        blockNum = Random.Range(0, 4);
        spriteRenderer.sprite = sprites[blockNum];
        Debug.Log(blockNum);
    }
}
