using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CarShopManager : MonoBehaviour
{

    //Car Info UI
    public Text carName, topSpeed, cost, lockedStatus;


    //Permissions
    public Button selectButton, claimButton;
    public CarInfo[] cars;


    //Car Spawning
    public Transform carPos;
    GameObject currentSpawnedCar;


    void Start()
    {
        SetUnlockStatus();
        ShowCar(0);
    }


    //All car unlock status Update
    private void SetUnlockStatus()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (PlayerPrefs.GetInt(cars[i].carID, 0) == 1)
            {
                cars[i].isUnlocked = true;
                cars[i].costToUnlock = 0;      
            }
            else
            {
                cars[i].isUnlocked = false;
            }
        }
    }


    public void WatchAndStart()
    {
        AdsManager.instance.ShowInterstitial(FindObjectOfType<CarShopManager>().SelectCar);
    }


    //Index
    int currentCarIndex;

    public void CarIndexChanger(int index)
    {
        if (currentCarIndex==cars.Length-1 && index==+1)
        {
            currentCarIndex = 0;
        }
        else if(currentCarIndex == 0 && index == -1)
        {
            currentCarIndex = cars.Length - 1;
        }
        else
        {
            currentCarIndex += index;
        }
        ShowCar(index);
    }
    //Showing Car
    void ShowCar(int dir)
    {
        if (currentSpawnedCar)
        {
            //LeanTween.moveX(currentSpawnedCar, -dir * 100, 1);
            Destroy(currentSpawnedCar);
        }
       
        currentSpawnedCar = Instantiate(cars[currentCarIndex].shopCarPrefab, carPos);
        carName.text = cars[currentCarIndex].name;
        topSpeed.text ="Top Speed: "+ cars[currentCarIndex].topSpeed.ToString() + "MPH";
        if (cars[currentCarIndex].costToUnlock>0)
        {
            cost.text = "To Unlock: " + cars[currentCarIndex].costToUnlock.ToString() + "$";
        }
        else
        {
            cost.text = "OWNED!";
        }
        LockStatusSteps(cars[currentCarIndex].isUnlocked);
    }

    //Shown Car Status Update 
    public void LockStatusSteps(bool isUnlocked)
    {
        if (isUnlocked)
        {
            lockedStatus.text = "Unlocked";
        }
        else
        {
            lockedStatus.text = "Locked";
        }

        selectButton.gameObject.SetActive(isUnlocked);
        claimButton.gameObject.SetActive(!isUnlocked);

    }

    public void SelectCar()
    {
        Scenemanager.Instance.LoadScene(1);
        //Scenemanager.Instance.LoadScene("City Choosing Menu");
        ////'''''''''''''////
        ///Selecting Car////
        PlayerPrefs.SetString("SelectedCarIndex", cars[currentCarIndex].carID);
        Debug.Log(PlayerPrefs.GetString("SelectedCarIndex"));
    }
    public void BuyCar()
    {
        if (MainMenuManager.Instance.currentScore >= cars[currentCarIndex].costToUnlock)
        {
            MainMenuManager.Instance.Buy(cars[currentCarIndex].costToUnlock);
            PlayerPrefs.SetInt(cars[currentCarIndex].carID, 1);
            LockStatusSteps(true);
            SetUnlockStatus();
        }

    }

}




[System.Serializable]
public class CarInfo
{
    public string name;
    public string carID;
    public bool isUnlocked;
    public GameObject shopCarPrefab;
    public int costToUnlock;
    public float topSpeed;
    public float torque;
}
