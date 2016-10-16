using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroScript : MonoBehaviour {
    public float introTime = 5.0f;
    private float timer;
    private GameManager gameManager;
    private Text timerText;

	// Use this for initialization
	void Start () {
        timer = introTime;
        gameManager = FindObjectOfType<GameManager>();
        timerText = GameObject.FindGameObjectWithTag("IntroTimerText").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timerText.text = "Zero";
        }else if(timer <= 1)
        {
            timerText.text = "One";
        }
        else if (timer <= 2)
        {
            timerText.text = "Two";
        }
        else if (timer <= 3)
        {
            timerText.text = "Three";
        }
        else if (timer <= 4)
        {
            timerText.text = "Four";
        }

        if (timer <= 0)
        {
            gameManager.StartPicking();
            this.gameObject.SetActive(false);
        }
	}
}
