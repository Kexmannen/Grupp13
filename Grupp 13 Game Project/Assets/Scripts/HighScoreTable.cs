using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;
    private void Awake()
    {
        //entryContainer = transform.Find("HighScoreEntryContainer");
        //entryTemplate = transform.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

   
        highScoreEntryTransformList = new List<Transform>();
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        //sort entrylist by score
        for(int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for(int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
            {
                if (highScores.highScoreEntryList[j] > highScores.highScoreEntryList[i])
                {
                    //swap
                    int temp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = temp;
                }
            }
        }

        foreach (int i in highScores.highScoreEntryList)
        {
            createHighScoreEntry(i, entryContainer, highScoreEntryTransformList);
        }

    }

    private void createHighScoreEntry(int score, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        int entryScore = score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        //Visuals, temporary
        entryTransform.Find("BG").gameObject.SetActive(rank % 2 == 1); //sets bg active if the rank is odd number (1st, 3rd etc)

        if (rank == 1) //highlight rank 1/highest score
        {
            entryTransform.Find("PosText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
        }

        switch (rank) //set trophy icon for 1st, 2nd and 3rd place
        {
            default:
                entryTransform.Find("TrophyIcon").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("TrophyIcon").GetComponent<Image>().color = new Color32(231, 189, 10, 255); //gold
                break;
            case 2:
                entryTransform.Find("TrophyIcon").GetComponent<Image>().color = new Color32(128, 128, 145, 255); //silver
                break;
            case 3:
                entryTransform.Find("TrophyIcon").GetComponent<Image>().color = new Color32(212, 85, 23, 255); //bronze
                break;

        }


        transformList.Add(entryTransform);
    }

    private void addHighScoreEntry(int score)
    {
        //Load saved highscores
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        //Add new entry to highscores
        highScores.highScoreEntryList.Add(score);
        //Save updated list
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();

    }

    private class HighScores
    {
        public List<int> highScoreEntryList;
    }
}
