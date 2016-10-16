using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
    public int TIMELIMIT = 100;
    public float timeRemaining;
    private GameManager gameManager;
    private Text timerText;

	// Use this for initialization
	void Start () {
        timeRemaining = TIMELIMIT;
        gameManager = FindObjectOfType<GameManager>();
        timerText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        if(gameManager.GetGameState() == GameManager.GameState.END || gameManager.GetGameState() == GameManager.GameState.START)
        {
            return;
        }

        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            timeRemaining = 0;
            gameManager.TimeOver();
        }timerText.text = "Time Remaining\n" + ((int)timeRemaining).ToString();
	}
}
