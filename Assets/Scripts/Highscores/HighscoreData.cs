using System.Collections.Generic;
using System;
using System.Xml.Serialization;

[XmlRoot("HighscoreData")] public class HighscoreData {
    [XmlArray("Level1HighScores")]
    [XmlArrayItem("Score")]
    public int[] level1HighScores;

    [XmlArray("Level1Names")]
    [XmlArrayItem("Name")]
    public string[] level1Names;

    [XmlArray("Level2HighScores")]
    [XmlArrayItem("Score")]
    public int[] level2HighScores;

    [XmlArray("Level2Names")]
    [XmlArrayItem("Name")]
    public string[] level2Names;

    [XmlArray("Level3HighScores")]
    [XmlArrayItem("Score")]
    public int[] level3HighScores;

    [XmlArray("Level3Names")]
    [XmlArrayItem("Name")]
    public string[] level3Names;

    [XmlArray("Level4HighScores")]
    [XmlArrayItem("Score")]
    public int[] level4HighScores;

    [XmlArray("Level4Names")]
    [XmlArrayItem("Name")]
    public string[] level4Names;

    [XmlArray("Level5HighScores")]
    [XmlArrayItem("Score")]
    public int[] level5HighScores;

    [XmlArray("Level5Names")]
    [XmlArrayItem("Name")]
    public string[] level5Names;
}
