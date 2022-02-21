using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Passenger : MonoBehaviour
{
    public int money;
    // Start is called before the first frame update
    void Start()
    {
        money = Random.Range(4, 12);
    }

    public void GetInSideCar()
    {
        Destroy(gameObject);
    }
}
