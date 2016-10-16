using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class HighscoreManager {

    //The instance of the singleton class
    private static HighscoreManager instance;

    //The saved game information
    private HighscoreData data;

    //Returns the instance of the singleton class
    public static HighscoreManager getHighscoreManager()
    {
        if (instance == null)
        {
            instance = new HighscoreManager();
        }
        return instance;
    }

    public bool IsHighScore(int level, int score)
    {
        getInfo();
        int[] scoreArray;

        if(level == 1)
        {
            scoreArray = data.level1HighScores;
        }else if(level == 2)
        {
            scoreArray = data.level2HighScores;
        }else if(level == 3)
        {
            scoreArray = data.level3HighScores;
        }else if(level == 4)
        {
            scoreArray = data.level4HighScores;
        }else
        {
            scoreArray = data.level5HighScores;
        }

        for(int i = 0; i < 5; i++)
        {
            if(score > 0 && score > scoreArray[i])
            {
                return true;
            }
        }return false;
    }

    public void AddHighScore(int level, int score, string name)
    {
        getInfo();
        int[] scoreArray;
        string[] nameArray;

        if (level == 1)
        {
            scoreArray = data.level1HighScores;
            nameArray = data.level1Names;
        }
        else if (level == 2)
        {
            scoreArray = data.level2HighScores;
            nameArray = data.level2Names;
        }
        else if (level == 3)
        {
            scoreArray = data.level3HighScores;
            nameArray = data.level3Names;
        }
        else if (level == 4)
        {
            scoreArray = data.level4HighScores;
            nameArray = data.level4Names;
        }
        else
        {
            scoreArray = data.level5HighScores;
            nameArray = data.level5Names;
        }

        for (int i = 0; i < 5; i++)
        {
            if (score > scoreArray[i])
            {
                for(int j = 4; j > i; j--)
                {
                    scoreArray[j] = scoreArray[j - 1];
                    nameArray[j] = nameArray[j - 1];
                }scoreArray[i] = score;
                nameArray[i] = name;
                save();
                return;
            }
        }
    }

    //Saves the info object to the application persistent data path, so will automatically
    //chose the correct path based on build settings
    private void save()
    {
        //Serialises the file
        XmlSerializer serializer = new XmlSerializer(typeof(HighscoreData));
        //Accesses the file
        FileStream file = File.Open(Application.persistentDataPath + "/save.xml", FileMode.Create);

        //Serialise
        serializer.Serialize(file, data);
        //Close the file
        file.Dispose();
    }

    //Returns saved game info
    //Initialises it to default values if no info found
    //Should only be called after trying to load saved game
    public HighscoreData getInfo()
    {
        //If no info has been loaded
        if (data == null)
        {
            //If saved file exists then load save file
            if (File.Exists(Application.persistentDataPath + "/save.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(HighscoreData));
                FileStream file = File.Open(Application.persistentDataPath + "/save.xml", FileMode.Open);
                data = (HighscoreData)serializer.Deserialize(file);
                file.Dispose();
            }//Otherwise create new highscore file with empty scores
            else
            {
                data = new HighscoreData();
                int[] tempIntArray = new int[5];
                string[] tempStringArray = new string[5];
                initialiseArrays(tempIntArray, tempStringArray);
                data.level1HighScores = tempIntArray;
                data.level1Names = tempStringArray;
                tempIntArray = new int[5];
                tempStringArray = new string[5];
                initialiseArrays(tempIntArray, tempStringArray);
                data.level2HighScores = tempIntArray;
                data.level2Names = tempStringArray;
                tempIntArray = new int[5];
                tempStringArray = new string[5];
                initialiseArrays(tempIntArray, tempStringArray);
                data.level3HighScores = tempIntArray;
                data.level3Names = tempStringArray;
                tempIntArray = new int[5];
                tempStringArray = new string[5];
                initialiseArrays(tempIntArray, tempStringArray);
                data.level4HighScores = tempIntArray;
                data.level4Names = tempStringArray;
                tempIntArray = new int[5];
                tempStringArray = new string[5];
                initialiseArrays(tempIntArray, tempStringArray);
                data.level5HighScores = tempIntArray;
                data.level5Names = tempStringArray;

                this.save();
            }
        }
        return data;
    }

    private void initialiseArrays(int[] intArray, string[] stringArray)
    {
        for(int i=0; i < 5; i++)
        {
            intArray[i] = -1;
            stringArray[i] = "";
        }
    }

    //Updates the saved game information
    public void updateInfo(HighscoreData newInfo)
    {
        data = newInfo;
        save();
    }
}
