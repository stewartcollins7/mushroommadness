using UnityEngine;
using System.Collections;

public class HungerBar : MonoBehaviour {

    private GameManager gameManager;
    public int MAXHUNGER = 100;
    public float HUNGERDECREASESPEED = 5f;
    public int POISONPENALTY = 33;
    public int EDIBLEBONUS = 20;
    private float hunger;
    private RectTransform hungerBar;

    private bool tripping = false;

    public void StartTripping()
    {
        tripping = true;
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        hunger = MAXHUNGER;
        hungerBar = this.gameObject.GetComponent<RectTransform>();
    }

    public void EatPoisonousShroom()
    {
        if (tripping)
        {
            tripping = false;
        }else
        {
            hunger -= POISONPENALTY;
        }
        
    }

    public void EatEdibleShroom()
    {
        hunger += EDIBLEBONUS;
    }

    void Update()
    {
        if(gameManager.GetGameState() == GameManager.GameState.START)
        {
            return;
        }
        if (tripping)
        {
            hunger -= Time.deltaTime * HUNGERDECREASESPEED / 2;
        }else
        {
            hunger -= Time.deltaTime * HUNGERDECREASESPEED;
        }
        
        if(hunger <= 0)
        {
            gameManager.HungerDepleted();
            hunger = 0;
        }Vector3 newScale = hungerBar.localScale;
        newScale.x = hunger / MAXHUNGER;
        hungerBar.localScale = newScale; 
    }
}
