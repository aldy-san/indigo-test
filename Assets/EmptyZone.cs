using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EmptyZone : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    RandomBlock randomBlock;
    ScoreText scoreText;
    GameManager gameManager;

    public Sprite initialSprite;
    public int rowNum = 0;
    public int colNum = 0;
    string[] listBlock = { "Rock", "Bishop", "Knight", "Dragon" };
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        randomBlock = GameObject.Find("Random Block").GetComponent<RandomBlock>();
        scoreText = GameObject.Find("Score").GetComponent<ScoreText>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnMouseOver()
    {
        if (!gameManager.slots[colNum,rowNum])
        {
            spriteRenderer.color = new Color(0.6f, 1f, 0.64f);
            if (Input.GetMouseButtonDown(0) && !gameManager.isLose){
                spriteRenderer.sprite = randomBlock.sprites[randomBlock.blockNum];
                if (checkAvailable())
                {
                    gameManager.countBlock[randomBlock.blockNum]++;
                    gameObject.tag = listBlock[randomBlock.blockNum];
                    Debug.Log(gameObject.tag);
                    Debug.Log(gameManager.countBlock[randomBlock.blockNum] == 3);
                    if (gameManager.countBlock[randomBlock.blockNum] == 3)
                    {
                        gameManager.countBlock[randomBlock.blockNum] = 0;
                        GameObject[] resetBlocks = GameObject.FindGameObjectsWithTag(listBlock[randomBlock.blockNum]);
                        foreach (GameObject obj in resetBlocks)
                        {
                            obj.GetComponent<EmptyZone>().reset();
                        }
                    }
                    if(randomBlock.blockNum == 0 || randomBlock.blockNum == 1)
                    {
                        scoreText.addScore(2);
                    } else
                    {
                        scoreText.addScore(1);
                    }
                } else
                {
                    // loseeeeer
                    gameManager.LoseHandler();
                    Debug.Log("kalah");
                }
                randomBlock.randomSlot();
                gameManager.slots[colNum, rowNum] = true;
                spriteRenderer.color = new Color(1f, 1f, 1f);
                gameManager.resetTimer();
            }
        }
    }
    void OnMouseExit()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f);
    }

    bool checkAvailable()
    {
        // check rock
        if (randomBlock.blockNum == 0)
        {
            for (int i = 0; i < 9; i++)
            {
                if (gameManager.slots[i, rowNum] && !gameManager.slots[colNum, rowNum])
                {
                    return false;
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (gameManager.slots[colNum, i] && !gameManager.slots[colNum, rowNum])
                {
                    return false;
                }
            }
        }
        //check bishop
        else if (randomBlock.blockNum == 1)
        {
            for (int i = 0; i < 6; i++)
            {
                int rowDiff = Mathf.Abs(rowNum - i); 
                //kiri
                if (colNum - rowDiff > 0  && i != rowNum)
                {
                    if (gameManager.slots[colNum - rowDiff, i])
                    {
                        return false;
                    }
                }
                //kanan
                if (colNum + rowDiff < 9 && i != rowNum)
                {
                    if (gameManager.slots[colNum + rowDiff, i])
                    {
                        return false;
                    }
                }
            }
        }
        //check knight
        else if (randomBlock.blockNum == 2)
        {
            int[] X = { 2, 1, -1, -2,
                   -2, -1, 1, 2 };
            int[] Y = { 1, 2, 2, 1,
                   -1, -2, -2, -1 };

            for (int i = 0; i < 8; i++)
            {
                int x = colNum + X[i];
                int y = rowNum + Y[i];
                if (x >= 0 && y >= 0 && y < 6 && x < 9)
                {
                    if (gameManager.slots[x, y])
                    {
                        return false;
                    }
                }
            }
        }
        //check dragon
        else if (randomBlock.blockNum == 3)
        {
            int[] X = { -1, 0, 1, -1,
                   1, -1, 0, 1 };
            int[] Y = { 1, 1, 1, 0,
                   0, -1, -1, -1 };

            for (int i = 0; i < 8; i++)
            {
                int x = colNum + X[i];
                int y = rowNum + Y[i];
                if (x >= 0 && y >= 0 && y < 6 && x < 9)
                {
                    if (gameManager.slots[x, y])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public void reset()
    {
        spriteRenderer.sprite = initialSprite;
        gameObject.tag = "empty";
        gameManager.slots[colNum, rowNum] = false;
    }
}
