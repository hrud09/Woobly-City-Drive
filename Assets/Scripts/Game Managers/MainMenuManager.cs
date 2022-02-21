using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    public Text Score;
    public int currentScore;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        currentScore = PlayerPrefs.GetInt("Score",5000);
        Score.text = currentScore.ToString();
    }

    //UI Manager Portion
    public void Buy(int toDecrease)
    {
        currentScore -= toDecrease;
        Score.text = currentScore.ToString();
        PlayerPrefs.SetInt("Score", currentScore);      
    }

}
