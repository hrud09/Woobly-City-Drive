using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    //Scoring
    public int maximumKnockOutNeedToEndLevel;
    public int ingameCurrentScore;
    public int deathPerPoint;
    int currentTotalScore;



    //Reactions With Scores
    GameObject reactions;
    public Sprite[] reactionImages;
    public Transform reactionSpawnPos;


    //boolean to chack game finish
    public bool isGameOver;


    public Text score;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    void Start()
    {
        currentTotalScore = PlayerPrefs.GetInt("Score");
        reactions = new GameObject();      
        reactions.AddComponent<Image>();
        score.text = currentTotalScore.ToString();
    
    }


    public void MoneyScore(int toIncrease)
    {
        if (isGameOver)
        {
            return;
        }
       
        GameObject r = Instantiate(reactions, reactionSpawnPos);
        r.GetComponent<Image>().sprite = reactionImages[Random.Range(0, reactionImages.Length)];
        LeanTween.moveLocal(r, (Vector3.up+Vector3.right) * Random.Range(90,150), 2);
        currentTotalScore += toIncrease;
        score.text = currentTotalScore.ToString();
        PlayerPrefs.SetInt("Score",currentTotalScore);
        Destroy(r, 2);
    }
}
