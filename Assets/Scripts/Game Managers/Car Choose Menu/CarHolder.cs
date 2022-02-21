using UnityEngine.EventSystems;
using UnityEngine;

public class CarHolder : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
       RotateBase();      
    }

    public float speed;
    Vector2 InitialPos;
    Vector3 Direction;
    float acceleration;
   
    private void OnMouseDown()
    {
        InitialPos = Input.mousePosition;     
    }
    private void RotateBase()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        acceleration = Mathf.Clamp(acceleration, 0, 1);
        if (Input.GetMouseButtonUp(0))
        {
            InitialPos = Vector2.zero;
            acceleration = 0;
        }
    
        if (Input.GetMouseButton(0))
        {
            if (Direction.magnitude > 1)
            {
                acceleration = 1;
            }
            Direction = ((Vector2)(Input.mousePosition) - InitialPos);
        }
        else
        {
            Deaccelerate();
        }
        rb.AddTorque(-acceleration * speed * Direction * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    private void Deaccelerate()
    {
        if (acceleration > 0)
        {
            acceleration -= Time.deltaTime * 0.8f;
        }
    }
}
