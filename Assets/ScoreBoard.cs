using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour, IAddScore
{
    public Information[] information;

    private void Start()
    {
        
    }

    private void Update()
    {
        AddScoreToBoard(1, "Test.JSON" , 2000);
    }

    public void AddScoreToBoard(int index, string username, int score)
    {
        information[index].CreateInformation(username, score);
    }
}

public class Information
{
    string userName;
    int position;
    int score;

    public void CreateInformation(string _username, int _score)
    {
        userName = _username;
        score = _score;
    }

    public Information CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Information>(jsonString);
    }
}

interface IAddScore
{
    void AddScoreToBoard(int index, string username, int score);
}