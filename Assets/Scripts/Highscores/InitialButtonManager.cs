using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitialButtonManager : MonoBehaviour {
    public Text firstInitial;
    public Text secondInitial;
    public Text thirdInitial;

	// Use this for initialization
	void Start () {
	
	}

    void ChangeLetterUp(Text initialText)
    {
        char letter = initialText.text.ToCharArray()[0];
        if(letter == 'A')
        {
            letter = 'Z';
        }else
        {
            letter = (char)((int)letter - 1);
        }initialText.text = letter.ToString();
    }

    void ChangeLetterDown(Text initialText)
    {
        char letter = initialText.text.ToCharArray()[0];
        if (letter == 'Z')
        {
            letter = 'A';
        }
        else
        {
            letter = (char)((int)letter - 1);
        }
        initialText.text = letter.ToString();
    }
	
	public void FirstInitialUp()
    {
        ChangeLetterUp(firstInitial);
    }

    public void FirstLetterDown()
    {
        ChangeLetterDown(firstInitial);
    }

    public void SecondInitialUp()
    {
        ChangeLetterUp(secondInitial);
    }

    public void SecondLetterDown()
    {
        ChangeLetterDown(secondInitial);
    }

    public void ThirdInitialUp()
    {
        ChangeLetterUp(thirdInitial);
    }

    public void ThirdLetterDown()
    {
        ChangeLetterDown(thirdInitial);
    }

    public void SubmitInitials()
    {
        LevelCompleteManager mgr = FindObjectOfType<LevelCompleteManager>();
        string initials = firstInitial.text.ToCharArray()[0].ToString() + secondInitial.text.ToCharArray()[0].ToString() + thirdInitial.text.ToCharArray()[0].ToString();
        mgr.SubmitHighScore(initials);
    }


}
