using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    public string ID;
    public float speed;
    public float RotationSpeed;
    Rigidbody rb;
    [SerializeField]Vector3 Direction;
    float acceleration;
    float waitTimeBeforeHorn,wait;

    public bool hasFoundPlayer;
    public GameObject target;

    AudioSource[] aud;
    [SerializeField] LayerMask targetMask;
    private void Start()
    {
        aud = GetComponents<AudioSource>();
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }


    void Update()
    {

        FindHuman(10, 360);
 
        if (!hasFoundPlayer && wait<=0.1f)
        {
            Direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            wait = Random.Range(2, 5);
          
        }
        else
        {
            wait -= Time.deltaTime;
        }
      
        if (waitTimeBeforeHorn > 0)
        {
            waitTimeBeforeHorn -= Time.deltaTime;
        }

    }
    void FindHuman(float _viewRadius, float _viewAngle)
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
          
            if (targetsInViewRadius[i].CompareTag("Human"))
            {

                if (!this.target)
                {

                    this.target = targetsInViewRadius[i].gameObject;

                    Direction = (this.target.transform.position - transform.position);
                    
                }

            
                Transform target = targetsInViewRadius[i].transform;
                target.GetComponent<Human>().hasToRun = true;
                target.GetComponent<Human>().car = this.gameObject.transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
                {
                    hasFoundPlayer = true;
                    if (waitTimeBeforeHorn <= 0)
                    {
                        if (!aud[0].isPlaying)
                        {
                            aud[0].Play();
                        }
                        waitTimeBeforeHorn = Random.Range(3, 5);
                    }

                }
                else
                {
                    hasFoundPlayer = false;
                }
                

            }
        
        }
    }

    private void LateUpdate()
    {
        acceleration = Mathf.Clamp(acceleration, 0, 1);
    }
    private void Accelerate()
    {
        acceleration += Time.deltaTime * 0.7f;
        aud[1].volume = acceleration + 0.1f;
    }
    private void Deaccelerate()
    {
        if (acceleration > 0)
        {
            acceleration -= Time.deltaTime * 0.8f;
        }
    }
    private void FixedUpdate()
    {

            Accelerate();
            Vector3 direction = new Vector3(Direction.x, 0, Direction.y);
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), RotationSpeed * Time.deltaTime);
            rb.velocity = transform.forward * speed* acceleration * 10 * Time.fixedDeltaTime;

        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Human"))
        {
            aud[2].Play();
            collision.collider.GetComponent<Human>().Die();

        }

    }

}
