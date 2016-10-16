using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ToMainMenuEvent : MonoBehaviour {

	public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
