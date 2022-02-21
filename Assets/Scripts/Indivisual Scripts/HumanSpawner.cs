using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{

    // Update is called once per frame
    GameObject Human;
    private void Start()
    {
        Human = HumanSpawnerManager.Instance.Human;
        StartCoroutine(SpawnHuman());
    }
    public IEnumerator SpawnHuman()
    {
        yield return new WaitForSeconds(Random.Range(2f,4f));
        Instantiate(Human, transform);

    }

}
