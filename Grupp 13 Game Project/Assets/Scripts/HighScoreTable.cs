using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;
    private List<int> highScoreEntryList;
    private void Awake()
    {
        //entryContainer = transform.Find("HighScoreEntryContainer");
        //entryTemplate = transform.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

   
        highScoreEntryTransformList = new List<Transform>();
        highScoreEntryList = new List<int>();

//#if UNITY_EDITOR
//        //AddHighScoreEntry(64623, "namn");
//        //AddHighScoreEntry(164623, "namn");
//        //AddHighScoreEntry(264623, "namn");
//        //AddHighScoreEntry(364623, "namn");
//        //AddHighScoreEntry(96464623, "namn");
//        //AddHighScoreEntry(3364623, "namn");
//#endif

        //create a key to hold and save data
        string jsonString = PlayerPrefs.GetString("highScoreTable");

        //must be stored within an object
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        Debug.Log(jsonString);

        //sort entrylist by score
        //for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        //{
        //    for (int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
        //    {
        //        if (highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
        //        {
        //            //swap
        //            int temp = highScores.highScoreEntryList[i].score;
        //            highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
        //            highScores.highScoreEntryList[j].score = temp;
        //        }
        //    }
        //}

        highScores.highScoreEntryList = highScores.highScoreEntryList.OrderByDescending(o => o.score).ToList();


        //keep only top 5 entries
        if (highScores.highScoreEntryList.Count > 5)
        {
            for (int h = highScores.highScoreEntryList.Count; h > 5; h--)
            {
                highScores.highScoreEntryList.RemoveAt(5);
            }
        }

        //create visual representation (graphics) for each highscore-entry
        foreach (var i in highScores.highScoreEntryList)
        {
            createHighScoreEntry(i.score, entryContainer, highScoreEntryTransformList);
        }

    }

    private void createHighScoreEntry(int score, Transform container, List<Transform> transformList)
    {
        float templateHeight = 35f;
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

    //public void AddHighScoreEntry(int points, string name)
    //{
    //    HighScore score = new HighScore { score = points, name = name };
    //    //Load saved highscores
    //    string jsonString = PlayerPrefs.GetString("highScoreTable");
    //    HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
    //    if (highScores == null)
    //    {
    //        highScores = new HighScores();
    //    }
    //    //Add new entry to highscores
    //    highScores.highScoreEntryList.Add(score);

    //    //Save updated list
    //    string json = JsonUtility.ToJson(highScores);
    //    PlayerPrefs.SetString("highScoreTable", json);
    //    PlayerPrefs.Save();

    //}
}
//kolla upp sparning av jsondata i assetsmappen, kolla skillnad mellan class och struct