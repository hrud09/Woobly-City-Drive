using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float timer=5;
    public bool gameOver;
    public Text timerText;


 
    public GameObject gameOverPannel;
    public GameObject buttons;
    public Text gameOverText;

    public bool showingAds;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 5;
       
    }

    float timeCount;
    // Update is called once per frame
    void Update()
    {
        if (timer<=0)
        {
            gameOver = true;
            gameOverText.text = "Level Failed'";
            gameOverPannel.SetActive(true);
            buttons.SetActive(true);
            if (timeCount<=0 && !showingAds)
            {
                LoadMenu();
            }
            else
            {
                timeCount -= Time.deltaTime;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.FloorToInt(timer / 60).ToString() + " : " + Mathf.FloorToInt(timer % 60).ToString();
        }
    }

    public void Test()
    {
        Debug.Log("Hello MF");
    }
  
    public void RegeneratePlayer(float t)
    {
        timer += t;
        gameOver = false;
        gameOverPannel.SetActive(false);
    }

   

    public void GameWon()
    {
        gameOver = true;
        gameOverText.text = "Level Passed";
        buttons.SetActive(false);
        gameOverPannel.SetActive(true);
        LoadMenu();
    }
    public void LoadMenu()
    {
   
        SceneManager.LoadScene(0);
    }
}
