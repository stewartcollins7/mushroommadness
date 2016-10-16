using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelCompleteText : MonoBehaviour {

    public int CORRECTMUSHROOMPOINTS = 250;
    public int INCORRECTMUSHROOMPENALTY = 100;
	// Use this for initialization
	void Start () {
        
        Text levelCompleteText = this.gameObject.GetComponent<Text>();
        CurrentLevelInfo levelInfo = FindObjectOfType<CurrentLevelInfo>();
        string completeInfo = "Different Species Found: \t\t\t\t\t" + levelInfo.pickedMushrooms.Count;
        foreach(KeyValuePair<MushroomScript.MushroomName,int> item in levelInfo.pickedMushrooms)
        {
            if(item.Key == MushroomScript.MushroomName.BUTTON)
            {
                completeInfo += "\nButton Mushrooms Picked:\t\t\t\t\t" + item.Value; 
            }else if(item.Key == MushroomScript.MushroomName.DEATHCAP)
            {
                completeInfo += "\nDeath Cap Mushrooms Picked:\t\t\t" + item.Value;
            }else if(item.Key == MushroomScript.MushroomName.PANTHER)
            {
                completeInfo += "\nPanther Cap Mushrooms Picked:\t\t" + item.Value;
            }
        }int desiredMushroomsPicked = levelInfo.pickedMushrooms[levelInfo.desiredMushroom];
        int score = desiredMushroomsPicked * CORRECTMUSHROOMPOINTS;
        completeInfo += "\n\nDesired Mushrooms Picked:\t\t\t\t" + desiredMushroomsPicked + "\t\t\t+"+score;
        int penalty = (levelInfo.mushroomCount - desiredMushroomsPicked) * INCORRECTMUSHROOMPENALTY;
        completeInfo += "\nIncorrect Mushrooms Picked:\t\t\t\t" + (levelInfo.mushroomCount - desiredMushroomsPicked) + "\t\t\t-" + penalty;
        score -= penalty;
        completeInfo += "\nFinal Score:\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + score;

        levelCompleteText.text = completeInfo;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
