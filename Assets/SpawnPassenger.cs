using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPassenger : MonoBehaviour
{
    public Transform[] spawnPos;
    public GameObject passenger;
    public int passengerCount;
    public List<Transform> selectedTransform;
    public GameObject destination;
    // Start is called before the first frame update
    void Start()
    {
    
        while (selectedTransform.Count < 16)
        {
            int i = Random.Range(0, spawnPos.Length);
            if (!selectedTransform.Contains(spawnPos[i]))
            {
                selectedTransform.Add(spawnPos[i]);
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
            int i = Random.Range(0, spawnPos.Length);
            if (!selectedTransform.Contains(spawnPos[i]))
            {
                selectedTransform.Add(spawnPos[i]);
                Instantiate(passenger, spawnPos[i]);
            }
        }
      
        while (true)
        {
            int i = Random.Range(0, spawnPos.Length);
            if (!selectedTransform.Contains(spawnPos[i]))
            {
                Instantiate(destination, spawnPos[i]);
                break;
            }
        }
    }
}
