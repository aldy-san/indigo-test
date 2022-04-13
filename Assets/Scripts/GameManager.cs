using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject emptyBlock;
    public bool[,] slots = new bool[9, 6];
    public GameObject loseModal;
    public Transform container;
    public ScoreText scoreText;
    public bool isLose = false;
    public int[] countBlock = new int[4];
    public Slider timerSlider;
    private float time = 10.0f;
    void Start()
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j< 9; j++)
            {
                var newBlock = Instantiate(emptyBlock, new Vector3((j * 1f)-4.0f, (i * 1F)-2.5f, 0), Quaternion.identity, container);
                newBlock.GetComponent<EmptyZone>().rowNum = i;
                newBlock.GetComponent<EmptyZone>().colNum = j;
            }
        }
    }
    private void Update()
    {
        if (!isLose)
        {
            time = time - Time.deltaTime;
            timerSlider.value = time;
            if(timerSlider.value == 0)
            {
                LoseHandler();
            }
        }
    }
    public void resetTimer()
    {
        time = 10f;
        timerSlider.value = time;
    }
    public void LoseHandler()
    {
        loseModal.SetActive(true);
        isLose = true;
    }
    public void Retry()
    {
        loseModal.SetActive(false);
        foreach (Transform obj in container)
        {
            obj.gameObject.GetComponent<EmptyZone>().reset();
        }
        slots = new bool[9, 6];
        countBlock = new int[4];
        scoreText.resetScore();
        isLose = false;
        time = 10f;
    }

}
