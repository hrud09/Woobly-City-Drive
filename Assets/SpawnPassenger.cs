using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPassenger : MonoBehaviour
{
    public Transform[] passengerSpawnPos;
    public GameObject passenger;
    public int passengerCount;
    public List<Transform> selectedTransform;
    public GameObject destination;
    public Transform taxi;
    // Start is called before the first frame update
    void Start()
    {
    
        while (selectedTransform.Count < 16)
        {
            int i = Random.Range(0, passengerSpawnPos.Length);
            if (!selectedTransform.Contains(passengerSpawnPos[i]))
            {
                selectedTransform.Add(passengerSpawnPos[i]);
            }
        }
        for (int i = 0; i < selectedTransform.Count; i++)
        {
            passengerCount++;
            Instantiate(passenger, selectedTransform[i]);
        }
      
    }

    public void FindNewPosAndInstantiate(Transform pickedPassenger)
    {
        selectedTransform.Remove(pickedPassenger.parent);

        while (selectedTransform.Count < 16)
        {
            int i = Random.Range(0, passengerSpawnPos.Length);
            if (!selectedTransform.Contains(passengerSpawnPos[i]))
            {
                selectedTransform.Add(passengerSpawnPos[i]);
                Instantiate(passenger, passengerSpawnPos[i]);
            }
        }
        taxi = GameObject.FindGameObjectWithTag("Car").transform;
        while (true)
        {
            int i = Random.Range(0, passengerSpawnPos.Length);
            if (!selectedTransform.Contains(passengerSpawnPos[i]))
            {
                if (Vector3.Distance(taxi.position, passengerSpawnPos[i].position) >= 20)
                {
                    Instantiate(destination, passengerSpawnPos[i]);
                    break;
                }
            }


       
        }
    }
}
