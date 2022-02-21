using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class Human : MonoBehaviour
{

    HumanSpawner humanSpawner;
    [HideInInspector]
    public bool hasToRun, isDead;
    [HideInInspector]
    public Transform car;

    float runningSpeed;
    Animator anim;
    Collider iniCollider;
    SkinnedMeshRenderer skMesh;
    Collider[] colliders;
    Rigidbody[] rigidbodies;


    private void Start()
    {
        runningSpeed = Random.Range(2f, 3f);       
        anim = GetComponent<Animator>();
       
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        StartCoroutine(LateStart());
    }
    
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        try
        {
            humanSpawner = GetComponentInParent<HumanSpawner>();
        }
        catch
        {
            Debug.Log("Not Found");
        }
        iniCollider = GetComponent<Collider>();
        colliders = GetComponentsInChildren<Collider>();
        skMesh = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        RagdollActivation(true);
    }

    public void Destroy(float t)
    {
        Destroy(gameObject,t);
    } 

    public void Die()
    {
        if (isDead)
        {
            return;
        }

     isDead = true;
       
        anim.enabled = iniCollider.enabled = false;
        Destroy(4);
        skMesh.material.color = Color.red;
        if (humanSpawner)
        {
            StartCoroutine(humanSpawner.SpawnHuman());
        }
    }

    public void RagdollActivation(bool activate)
    {
        foreach (Collider item in colliders)
        {
            item.enabled = activate;
        }
        foreach (Rigidbody item in rigidbodies)
        {
            item.isKinematic = !activate;
        }
     
    }

    bool directionSet;
    Vector3 direction;
    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if (hasToRun)
        {
            if (!directionSet)
            {
                direction = car.forward;
                directionSet = true;             
            }
            Run();
        }
        else
        {
            anim.SetBool("Run", false);
            directionSet = false;
        }
    }
    private void Run()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
        anim.SetBool("Run",true);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, runningSpeed * Time.deltaTime);
        if (Vector3.Distance(car.transform.position,transform.position)>10)
        {
            hasToRun = false;
        }
    }
}
