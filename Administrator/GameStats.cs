using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

// Record all the statics in the game
public class GameStats
{
    private int _score;
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = $"{_score} / {winningScore}";
        }
    }

    private const int winningScore = 20;
    private Text scoreText;

    // Singleton
    private static GameStats _instance;
    public static GameStats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameStats();
            }
            return _instance;
        }
    }

    private GameStats()
    {
        scoreText = GameObject.Find(ObjectPath.score).GetComponent<Text>() as Text;
        Assert.IsNotNull(scoreText);
        score = 0;
    }

    // public void ComputeScore()
    // {
    //     foreach (MapObject obj in Map.Instance.mapObjects)
    //     {
    //         if (obj is Tower)
    //         {
    //             if (Map.Instance.GetTile(obj.rc).color != Color.Empty)
    //             {
    //                 score += 1;
    //             }
    //         }
    //     }
    // }
}
