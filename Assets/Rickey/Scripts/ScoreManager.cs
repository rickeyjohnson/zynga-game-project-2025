using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int antsKilled = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddAntKill()
    {
        antsKilled++;
    }

    public int GetScore()
    {
        return antsKilled;
    }
}
