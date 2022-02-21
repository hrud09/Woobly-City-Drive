using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialChanging : MonoBehaviour
{
    public static MaterialChanging Instance;
    public Material mat;
    public Transform car;
    public Material oldMat;
    GameObject blockedObj;   
    Camera cam;
    RaycastHit hit;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (!car)
        {
            return;
        }
        Ray ray = new Ray(cam.transform.position, car.position - cam.transform.position);
        float dis = Vector3.Distance(car.position, cam.transform.position);
        if (Physics.Raycast(ray,out hit,dis))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                blockedObj = hit.collider.gameObject;
                if (oldMat==null)
                {
                    oldMat = blockedObj.GetComponent<MeshRenderer>().material;
                }               
                ChangeMat();
            }
            else
            {
                if (blockedObj)
                {
                    RevertMat();
                }
            }
        }
    }
    public void ChangeMat()
    {

         mat.mainTexture = oldMat.mainTexture;
         blockedObj.GetComponent<MeshRenderer>().material = mat;

    }
    public void RevertMat()
    {
        blockedObj.GetComponent<MeshRenderer>().material = oldMat;
        blockedObj = null;
        oldMat = null;
       
    }


    private float timer;
    [SerializeField] private Text fpsText;
    private void LateUpdate()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text ="FPS: " + fps.ToString("00");
            timer = Time.unscaledTime + 0.5f;
        }
    }
}
