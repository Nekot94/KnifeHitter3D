﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{

    public static Highscores instance;

    void Start()
    {
        instance = this;
    }

    public void AddHighscores(int scores)
    {
        if(!PlayerPrefs.HasKey("Highscores") || PlayerPrefs.GetInt("Highscores") < scores)
        {
            PlayerPrefs.SetInt("Highscores", scores);
        }
    }

    public int GetHighscores()
    {
        if (PlayerPrefs.HasKey("Highscores"))
            return PlayerPrefs.GetInt("Highscores");
        return 0;
    }

}
