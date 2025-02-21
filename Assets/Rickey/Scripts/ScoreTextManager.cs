using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextManager : MonoBehaviour
{
    public Text scoreText;

    private void Update()
    {
        scoreText.text = "Ants Killed: " + ScoreManager.instance.GetScore();
    }
}
