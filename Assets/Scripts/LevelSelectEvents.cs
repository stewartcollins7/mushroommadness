using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectEvents : MonoBehaviour {

    private LevelInitialisationInfo level1Info = new LevelInitialisationInfo(MushroomScript.MushroomName.BUTTON, new Vector3(10, 0, 0), 1);
    private LevelInitialisationInfo level2Info = new LevelInitialisationInfo(MushroomScript.MushroomName.DEATHCAP, new Vector3(0, 10, 0), 2);
    private LevelInitialisationInfo level3Info = new LevelInitialisationInfo(MushroomScript.MushroomName.PANTHER, new Vector3(0, 0, 10), 3);
    private LevelInitialisationInfo level4Info = new LevelInitialisationInfo(MushroomScript.MushroomName.BUTTON, new Vector3(4, 1, 5), 4);
    private LevelInitialisationInfo level5Info = new LevelInitialisationInfo(MushroomScript.MushroomName.PANTHER, new Vector3(2, 6, 2), 5);

    private LevelInitialisationInfo selectedLevel = null;
    public Text levelDescriptionText;

    private class LevelInitialisationInfo {
        public MushroomScript.MushroomName desiredMushroomName;
        public MushroomScript.MushroomType desiredMushroomType;
        public Vector3 mushroomRatio;
        public int level;

        public LevelInitialisationInfo(MushroomScript.MushroomName name, Vector3 ratio, int level)
        {
            this.desiredMushroomName = name;
            if(name == MushroomScript.MushroomName.BUTTON)
            {
                this.desiredMushroomType = MushroomScript.MushroomType.EDIBLE;
            }else if(name == MushroomScript.MushroomName.DEATHCAP)
            {
                this.desiredMushroomType = MushroomScript.MushroomType.POISON;
            }else
            {
                this.desiredMushroomType = MushroomScript.MushroomType.TRIPPY;
            }
            this.mushroomRatio = ratio;
            this.level = level;
        }
    }

    public void SelectLevel1()
    {
        selectedLevel = level1Info;
        PrintLevelDescription();
    }

    public void SelectLevel2()
    {
        selectedLevel = level2Info;
        PrintLevelDescription();
    }

    public void SelectLevel3()
    {
        selectedLevel = level3Info;
        PrintLevelDescription();
    }

    public void SelectLevel4()
    {
        selectedLevel = level4Info;
        PrintLevelDescription();
    }

    public void SelectLevel5()
    {
        selectedLevel = level5Info;
        PrintLevelDescription();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayLevel()
    {
        if(selectedLevel == null)
        {
            levelDescriptionText.text = "Please select a level to play";
            return;
        }

        //Set level info then load level
        CurrentLevelInfo levelInfo = FindObjectOfType<CurrentLevelInfo>();
        levelInfo.desiredMushroom = selectedLevel.desiredMushroomName;
        levelInfo.levelNumber = selectedLevel.level;
        levelInfo.mushroomCount = 0;
        levelInfo.grassPicked = 0;
        levelInfo.leavesBrushed = 0;
        levelInfo.pickedMushrooms = new System.Collections.Generic.Dictionary<MushroomScript.MushroomName, int>();
        levelInfo.mushroomRatio = selectedLevel.mushroomRatio;
        SceneManager.LoadScene("Main");
    }

    private void PrintLevelDescription()
    {
        LevelInitialisationInfo levelInfo = selectedLevel;
        string descriptionText = "Target Mushroom: ";
        if(levelInfo.desiredMushroomName == MushroomScript.MushroomName.BUTTON)
        {
            descriptionText += "Button\nMushroom Type: Edible\n";
        }else if(levelInfo.desiredMushroomName == MushroomScript.MushroomName.DEATHCAP)
        {
            descriptionText += "Death Cap\nMushroom Type: Poisonous\n";
        }else
        {
            descriptionText += "Panther Cap\nMushroom Type: Hallucinogenic\n";
        }
        if(levelInfo.mushroomRatio.x == 10 || levelInfo.mushroomRatio.y == 10 || levelInfo.mushroomRatio.z == 10)
        {
            descriptionText += "Other Mushrooms Present: No";
        }else
        {
            descriptionText += "Other Mushrooms Present: Yes";
        }levelDescriptionText.text = descriptionText;
    }
}
