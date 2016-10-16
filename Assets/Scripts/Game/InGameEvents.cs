using UnityEngine;
using System.Collections;

public class InGameEvents : MonoBehaviour {

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnKeepItPressed()
    {
        gameManager.KeepIt();
    }

    public void OnTossItPressed()
    {
        gameManager.TossIt();
    }

    public void OnEatItPressed()
    {
        gameManager.EatIt();
    }
}
