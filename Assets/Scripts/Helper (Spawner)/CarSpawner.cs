using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarSpawner : MonoBehaviour
{

    public GameObject[] ingameCars;
    CinemachineVirtualCamera CMVCam;
    GameObject car;
   void Start()
   {
        CMVCam = FindObjectOfType<CinemachineVirtualCamera>();
        foreach (GameObject item in ingameCars)
        {
            if (item.GetComponent<Taxi>().ID==PlayerPrefs.GetString("SelectedCarIndex"))
            {
                car = Instantiate(item, transform);
                CMVCam.Follow = car.transform;
                CMVCam.LookAt = car.transform;
                MaterialChanging.Instance.car = car.transform;
            }
        }
    
   }

}
