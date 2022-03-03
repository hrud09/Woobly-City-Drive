using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float timer;
    public bool gameOver;
    public Text timerText;


 
    public GameObject gameOverPannel;
    public GameObject rewardedAdButton,nextButton;
    public Text gameOverText;

    public GameObject minimap;
   
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 60;
       
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (timer<=0)
            {
                gameOver = true;
minimap.SetActive(false);
                gameOverText.text = "Level Failed'";
                nextButton.SetActive(false);
                gameOverPannel.SetActive(true);
                rewardedAdButton.SetActive(true);
           

            }
            else
            {
                timer -= Time.deltaTime;
                timerText.text = Mathf.FloorToInt(timer / 60).ToString() + " : " + Mathf.FloorToInt(timer % 60).ToString();
            }
        }
      
    }
    
    public void RegeneratePlayer(float t)
    {
        timer = t;
        FindObjectOfType<ScoreManager>().minimumMoneyLimit += 10;
        gameOver = false;
        minimap.SetActive(true);
        gameOverPannel.SetActive(false);
    }

   

    public void GameWon()
    {
        gameOver = true;
        minimap.SetActive(false);
        gameOverText.text = "Level Passed";
        nextButton.SetActive(true);
        rewardedAdButton.SetActive(false);
        gameOverPannel.SetActive(true);
        
    }

   
    public void LoadMenu()
    {
   
        SceneManager.LoadScene(0);
    }

    public void LoadSameLevel()
    {
        SceneManager.LoadScene(1);
    }
}
