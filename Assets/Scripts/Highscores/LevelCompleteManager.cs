using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteManager : MonoBehaviour {
    public int GRASSPOINTS = 10;
    public int LEAFPOINTS = 15;
    public int CORRECTMUSHROOMPOINTS = 420;
    public int INCORRECTMUSHROOMPENALTY = 100;
    private CurrentLevelInfo levelInfo;
    public GameObject enterHighScorePanel;
    private HighscoreManager hsMgr;

    public Text scoreInfoText;
    public Text levelText;
    public Text desiredMushroomText;
    public InputField initialsInput;

    private int finalScore;
    // Use this for initialization
    void Start()
    {
        levelInfo = FindObjectOfType<CurrentLevelInfo>();
        hsMgr = HighscoreManager.getHighscoreManager();
        finalScore = calculateScore();
        if(!hsMgr.IsHighScore(levelInfo.levelNumber, finalScore)){
            enterHighScorePanel.SetActive(false);
            PrintHighscore();
        }levelText.text = "Level " + levelInfo.levelNumber;
        if(levelInfo.desiredMushroom == MushroomScript.MushroomName.BUTTON)
        {
            desiredMushroomText.text = "Button";
        }else if(levelInfo.desiredMushroom == MushroomScript.MushroomName.DEATHCAP)
        {
            desiredMushroomText.text = "Death Cap";
        }else
        {
            desiredMushroomText.text = "Panther Cap";
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SubmitHighScore(string initials)
    {
        Debug.Log("test");
        hsMgr.AddHighScore(levelInfo.levelNumber, finalScore, initials);
        Debug.Log("test2");
        enterHighScorePanel.SetActive(false);
        PrintHighscore();
    }

    public void PrintHighscore()
    {
        StartCoroutine(PrintHighScoreDetails());
    }

    private IEnumerator PrintHighScoreDetails()
    {
        float waitTime = 1f;
        scoreInfoText.text = "";
        yield return new WaitForSeconds(waitTime);
        string scoreInfo = "Grass Pulled\t\t\t" + levelInfo.grassPicked + "\t\tx10\t\t  " + (levelInfo.grassPicked * GRASSPOINTS);
        scoreInfoText.text = scoreInfo;
        yield return new WaitForSeconds(waitTime);
        scoreInfo += "\nLeaves Brushed\t\t\t" + levelInfo.leavesBrushed + "\t\tx15\t\t  " + (levelInfo.leavesBrushed * LEAFPOINTS);
        scoreInfoText.text = scoreInfo;
        yield return new WaitForSeconds(waitTime);
        scoreInfo += "\nTarget Mushrooms\t\t" + levelInfo.GetNumberOfDesiredMushrooms() + "\t\tx420\t\t  " + (levelInfo.GetNumberOfDesiredMushrooms() * CORRECTMUSHROOMPOINTS);
        scoreInfoText.text = scoreInfo;
        yield return new WaitForSeconds(waitTime);
        scoreInfoText.text = scoreInfo;
        int incorrectMushrooms = levelInfo.mushroomCount - levelInfo.GetNumberOfDesiredMushrooms();
        scoreInfo += "\nWrong Mushrooms\t\t" + incorrectMushrooms + "\t\tx-100\t\t";
        if (incorrectMushrooms == 0) {
            scoreInfo += "  0";
        }else
        {
            scoreInfo += incorrectMushrooms * (-INCORRECTMUSHROOMPENALTY);
        }
        scoreInfoText.text = scoreInfo;
        yield return new WaitForSeconds(waitTime);
        scoreInfo += "\n------------------------------\nTotal Score\t\t\t\t\t\t\t\t\t  " + finalScore;
        scoreInfoText.text = scoreInfo;
    }

    private int calculateScore()
    {
        int score = 0;

        score += levelInfo.leavesBrushed * LEAFPOINTS;
        score += levelInfo.grassPicked * GRASSPOINTS;
        score += levelInfo.GetNumberOfDesiredMushrooms() * CORRECTMUSHROOMPOINTS;
        score -= (levelInfo.mushroomCount - levelInfo.GetNumberOfDesiredMushrooms()) *INCORRECTMUSHROOMPENALTY;

        return score;
        
    }
}
