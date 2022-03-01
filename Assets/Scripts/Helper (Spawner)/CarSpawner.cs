using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CarSpawner : MonoBehaviour
{

    public GameObject[] ingameCars;
    CinemachineVirtualCamera CMVCam;
    GameObject car;
    public GameObject driver;
   void Start()
   {
        CMVCam = FindObjectOfType<CinemachineVirtualCamera>();
        CMVCam.Follow = driver.transform;
        CMVCam.LookAt = driver.transform;
        StartCoroutine(SetCar());
       
   }

   IEnumerator SetCar()
   {
       yield return new WaitForSeconds(1);
       driver.GetComponent<Animator>().SetBool("IsDriver",true);
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
       driver.transform.DOMove(car.transform.position, 2).OnComplete(() =>
       {
           Destroy(driver);
           
       });
    
   }
}
