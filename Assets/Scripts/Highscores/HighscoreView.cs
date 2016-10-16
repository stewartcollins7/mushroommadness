using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoreView : MonoBehaviour {
    private HighscoreData data;
    public Text level1Highscores;
    public Text level2Highscores;
    public Text level3Highscores;
    public Text level4Highscores;
    public Text level5Highscores;

    

    // Use this for initialization
    void Start () {
        data = HighscoreManager.getHighscoreManager().getInfo();
        int[] scoreArray;
        string[] nameArray;
        scoreArray = data.level1HighScores;
        nameArray = data.level1Names;
        PrintLevelScoreInfo(scoreArray, nameArray, level1Highscores);
        scoreArray = data.level2HighScores;
        nameArray = data.level2Names;
        PrintLevelScoreInfo(scoreArray, nameArray, level2Highscores);
        scoreArray = data.level3HighScores;
        nameArray = data.level3Names;
        PrintLevelScoreInfo(scoreArray, nameArray, level3Highscores);
        scoreArray = data.level4HighScores;
        nameArray = data.level4Names;
        PrintLevelScoreInfo(scoreArray, nameArray, level4Highscores);
        scoreArray = data.level5HighScores;
        nameArray = data.level5Names;
        PrintLevelScoreInfo(scoreArray, nameArray, level5Highscores);
    }

    private void PrintLevelScoreInfo(int[] scores, string[] names, Text textElement)
    {
        if(scores[0] <= 0)
        {
            textElement.text = "No highscores found";
            return;
        }
        else
        {
            string scoreText = "";

            for (int i = 0; i < 5; i++)
            {
                if(scores[i] >= 0)
                {
                    scoreText += names[i] + "\t\t" + scores[i] + "\n";
                }else
                {
                    break;
                }
            }textElement.text = scoreText;
        }
    }
	
	
}
