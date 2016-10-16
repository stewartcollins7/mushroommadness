using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable] public class HighscoreData {
    public int[] level1HighScores;
    public string[] level1Names;

    public int[] level2HighScores;
    public string[] level2Names;

    public int[] level3HighScores;
    public string[] level3Names;

    public int[] level4HighScores;
    public string[] level4Names;

    public int[] level5HighScores;
    public string[] level5Names;
}
