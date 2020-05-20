using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    public static HighScoreController instance = null;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    public void AddHighScoreEntry(int points, string name)
    {
        HighScore score = new HighScore { score = points, name = name };
        //Load saved highscores
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        if (highScores == null)
        {
            highScores = new HighScores();
        }
        //Add new entry to highscores
        highScores.highScoreEntryList.Add(score);

        //Save updated list
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();

    }

}

[System.Serializable]
public class HighScores
{
    public List<HighScore> highScoreEntryList;

    public HighScores()
    {
        highScoreEntryList = new List<HighScore>();
    }
    // public string ToString() { }
}

[System.Serializable]
public struct HighScore
{
    public int score;
    public string name;

}