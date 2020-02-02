using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leaderboard;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class LeaderBoard_Backend : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> usernames = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> scores = new List<TextMeshProUGUI>();
    [SerializeField] List<TMP_InputField> uName = new List<TMP_InputField>();
    LeaderBoard board = new LeaderBoard();

    public static Player player;

    public TextAsset jsonFile;
    public int newPos;

    string path = "Assets/Resources/Leaderboard.json";

    bool entered = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        string json = jsonFile.text;
        board = Newtonsoft.Json.JsonConvert.DeserializeObject<LeaderBoard>(json);

        int adder = 0;
        newPos = 0;

        foreach (Leaderboard.Score score in board.Scores)
        {
            usernames[score.positinon - 1].transform.parent.gameObject.SetActive(true);
            if (System.Convert.ToInt32(player.Score) > System.Convert.ToInt32(score.score) && !entered)
            {
                newPos = score.positinon;
                for (int i = newPos; i-- > newPos;)
                {
                    board.Scores[i].score = board.Scores[i - 1].score;
                    board.Scores[i].name = board.Scores[i - 1].name;
                }

                uName[score.positinon - 1].gameObject.SetActive(true);
                
                //score.score = player.Score;
                entered = true;
                adder += 1;

                scores[score.positinon - 1].text = player.Score;
            }

            if (score.positinon - 1 + adder < usernames.Count)
            {
                usernames[score.positinon - 1 + adder].text = score.name;
                scores[score.positinon - 1 + adder].text = score.score;
                scores[score.positinon - 1 + adder].transform.parent.gameObject.SetActive(true);
            }

            if(newPos != 0)
            {
                board.Scores[0].score = player.Score;
            }
        }

        if (board.Scores.Count == 0)
        {
            uName[0].gameObject.SetActive(true);
            board.Scores.Add(new Leaderboard.Score());
            usernames[0].GetComponentInChildren<Transform>().gameObject.SetActive(true);
            board.Scores[0].score = player.Score;
            board.Scores[0].positinon = 1;
            newPos = 1;
        }
    }

    public void AddInfo(string arg0)
    {
        board.Scores[newPos - 1].name = uName[newPos - 1].text; 
        usernames[newPos - 1].text = uName[newPos - 1].text;
        uName[newPos - 1].gameObject.SetActive(false);

        var str = JsonConvert.SerializeObject(board, Formatting.Indented);
        using (System.IO.StreamWriter sw = new StreamWriter("Assets/Resources/Leaderboard.json"))
        {
            sw.Write(str);
        }

        UnityEditor.AssetDatabase.Refresh();
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Environment_1", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
