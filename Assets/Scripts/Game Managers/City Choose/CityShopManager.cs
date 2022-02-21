using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CityShopManager : MonoBehaviour
{
    //City Info
    [SerializeField] Text cityName, area, cost, lockedStatus;
    //Index



    //Permissions
    public Button playButton, claimButton;
    public CityInfo[] cities;


    //City Spawning
    public Transform citySpawnedPos;
    GameObject currentSpawnedCity;


    void Start()
    {
        SetUnlockStatus();
        ShowCity();
    }


    //All city unlock status Update
    private void SetUnlockStatus()
    {
        for (int i = 0; i < cities.Length; i++)
        {
            if (PlayerPrefs.GetInt(cities[i].cityID, 0) == 1)
            {
                cities[i].isUnlocked = true;
                cities[i].costToUnlock = 0;
            }
            else
            {
                cities[i].isUnlocked = false;
            }
        }
    }




    int currentCityIndex;
    //Showing City
    public void CityIndexChanger(int index)
    {
        if (currentCityIndex == cities.Length - 1 && index == +1)
        {
            currentCityIndex = 0;
        }
        else if (currentCityIndex == 0 && index == -1)
        {
            currentCityIndex = cities.Length - 1;
        }
        else
        {
            currentCityIndex += index;
        }
        ShowCity();
    }
    void ShowCity()
    {
       // LeanTween.moveY(currentSpawnedCity, -100, 1);
        Destroy(currentSpawnedCity);
        currentSpawnedCity = Instantiate(cities[currentCityIndex].cityObj, citySpawnedPos);
        cityName.text = cities[currentCityIndex].name;
        area.text ="Toatal Area: " + cities[currentCityIndex].area.ToString() + "sq. Meter";
        if (cities[currentCityIndex].costToUnlock>0)
        {
            cost.text = "To Unlock: " + cities[currentCityIndex].costToUnlock.ToString() + "$";
        }
        else
        {
            cost.text = "OWNED!";
        }
        LockStatusSteps(cities[currentCityIndex].isUnlocked);
    }

    //Shown City Status Update 
    void LockStatusSteps(bool isUnlocked)
    {
        if (isUnlocked)
        {
            lockedStatus.text = "Unlocked";
        }
        else
        {
            lockedStatus.text = "Locked";
        }

        playButton.gameObject.SetActive(isUnlocked);
        claimButton.gameObject.SetActive(!isUnlocked);

    }

    public void BuyCity()
    {
        if (MainMenuManager.Instance.currentScore >= cities[currentCityIndex].costToUnlock)
        {
            MainMenuManager.Instance.Buy(cities[currentCityIndex].costToUnlock);
            PlayerPrefs.SetInt(cities[currentCityIndex].cityID, 1);
            LockStatusSteps(true);
            SetUnlockStatus();
        }

    }

    public void SelectCity()
    {
        Scenemanager.Instance.LoadScene(cities[currentCityIndex].name);
        ////'''''''''''''////
        ///Selecting Car////
    }

}
[System.Serializable]
public class CityInfo
{
    public string name;
    public string cityID;
    public bool isUnlocked;
    public GameObject cityObj;
    public int costToUnlock;
    public float area;
}
