using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawnerManager : MonoBehaviour
{
    public static HumanSpawnerManager Instance;
    public GameObject Human;
    private void Awake()
    {
        Instance = this;       
    }

}

