using UnityEngine;

public class Taxi : MonoBehaviour
{

    int money;

    public bool passengerIn;
    public string ID;
    public float speed;
    public float RotationSpeed;
    Rigidbody rb;
    Vector3 Direction;
    Vector2 InitialPos;
    float acceleration;
    float waitTimeBeforeHorn;
    ScoreManager scoremanager;

    AudioSource[] aud;
    [SerializeField] LayerMask targetMask;
    private void Start()
    {
        aud = GetComponents<AudioSource>();
        scoremanager = ScoreManager.Instance;
    }

    
    void Update()
    {

        //FindHuman(10,60);
        if (GameManager.Instance.gameOver)
        {
            return;
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (Input.GetMouseButtonDown(0))
        {
            InitialPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            InitialPos = Vector2.zero;
        }
        if (Input.GetMouseButton(0))
        {
            Direction= ((Vector2)(Input.mousePosition) - InitialPos);
        }
        else
        {
            Deaccelerate();
        }

        if (waitTimeBeforeHorn>0)
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
                Transform  target = targetsInViewRadius[i].transform;
                target.GetComponent<Human>().hasToRun = true;
                target.GetComponent<Human>().car = this.gameObject.transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
                {
                    if (waitTimeBeforeHorn<=0)
                    {
                        if (!aud[0].isPlaying)
                        {
                            aud[0].Play();
                        }
                        waitTimeBeforeHorn = Random.Range(3, 5);
                    }
                   
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
        acceleration += Time.deltaTime * 0.9f;
        aud[1].volume = acceleration+0.01f;
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
        if (GameManager.Instance.gameOver)
        {
            return;
        }
        if (Direction.magnitude > 0f)
        {
            Accelerate();
            Vector3 direction = new Vector3(Direction.x, 0, Direction.y);
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), RotationSpeed * Time.deltaTime);         
            rb.velocity = transform.forward * speed * 10 * acceleration * Time.fixedDeltaTime;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Human"))
        {
            aud[2].Play();        
            collision.collider.GetComponent<Human>().Die();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destination") && passengerIn)
        {
            passengerIn = false;
            scoremanager.MoneyScore(money);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Passenger") && !passengerIn)
        {
            passengerIn = true;
            money = other.GetComponent<Passenger>().money;
            FindObjectOfType<SpawnPassenger>().FindNewPosAndInstantiate(other.transform);
  
            other.GetComponent<Passenger>().GetInSideCar();
        }
    }
}
