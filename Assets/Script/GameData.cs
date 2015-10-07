using UnityEngine;
using System.Collections;

public class GameData : IGameData
{
    private int score;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {            
            score = value;

            if (score > HighestScore)
            {
                HighestScore = score;
            }
        }
    }

    public int HighestScore
    {
        get
        {
            return PlayerPrefs.GetInt("HighestScore");
        }
        set
        {
            PlayerPrefs.SetInt("HighestScore", value);
        }
    }

    private long currentShootTime; //millionseconds

    public long CurrentShootTime
    {
        get
        {
            return currentShootTime;
        }
        set
        {
            currentShootTime = value;
        }
    }

    private long playerShootTime;

    public long PlayerShootTime
    {
        get
        {
            return playerShootTime;
        }
        set
        {
            playerShootTime = value;
        }
    }

    private long roundFireBeginTime;

    public long RoundFireBeginTime
    {
        get
        {
            return roundFireBeginTime;
        }
        set
        {
            roundFireBeginTime = value;
        }
    }
}
