using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool fail = false;
    public static int trapCount = 0;

    [SerializeField]
    private TextMeshProUGUI waveText;
    private int wave = 0;
    private int waveNum = 3;
    private int sponeRange = 50;
    private int[] popNum;
    public int left = 0;
    [SerializeField]
    private Enemy enemy;

	// Use this for initialization
	void Start () {
        instance = this;
        trapCount = 0;
        popNum = new int[waveNum];
        popNum[0] = 10;
        popNum[1] = 20;
        popNum[2] = 30;
        Scheduler.instance.AddEvent(5, WaveStart);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void WaveStart()
    {
        for (int i = 0; i < popNum[wave]; i++)
        {
            var x = Random.Range(-sponeRange, sponeRange);
            var y = Random.Range(-sponeRange, sponeRange);
            var dir = Random.Range(0, 360);

            Instantiate(enemy, new Vector3(x, 0, y), Quaternion.Euler(0, dir, 0));
        }
        left = popNum[wave];
        wave++;
        waveText.text = "WAVE " + wave + " START";
        waveText.gameObject.SetActive(true);
        Scheduler.instance.AddEvent(1, FinShow);
    }

    private void FinShow()
    {
        waveText.gameObject.SetActive(false);
    }

    public void DecEnemy()
    {
        left--;
        if (left == 0)
        {
            if (wave == waveNum) GameClear();
            else Scheduler.instance.AddEvent(5, WaveStart);
        }
    }

    private void GameClear()
    {
        fail = false;
        SceneManager.LoadScene("EndScene");
    }

    public void GameOver()
    {
        Debug.Log("game over!");
        fail = true;
        SceneManager.LoadScene("EndScene");
    }
}
