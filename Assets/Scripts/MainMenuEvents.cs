using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour {

	public void ToHighScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void ToInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
