using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Animator animate;
    public GameObject scope;
    public GameObject gun;
    public GameObject bullet;
    public Transform pointBullet;
    public float zoomSpeed;
    private int BulletCout;
    private bool isWalk;
    private bool CanShoot;
    private bool GunShoot;
    private  Vector3 cameraForward ;
    private  Vector3 cameraRight ;
    public Rigidbody rb;
 

    // Start is called before the first frame update
    void Start()
    {
     
        rb = GetComponent<Rigidbody>();
        BulletCout =1500000;
        Physics.gravity *= 10f;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
        
    }
    void ControlMovement(){
        cameraForward = Camera.main.transform.forward;
        cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        if (Input.GetKey(KeyCode.W)){
            transform.Translate(cameraForward* speed *Time.deltaTime);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.S)){
            transform.Translate(-cameraForward * speed *Time.deltaTime);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.D)){
            transform.Translate(cameraRight* speed *Time.deltaTime);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.A)){
            transform.Translate(-cameraRight * speed *Time.deltaTime);
            isWalk = true;
        }
        RunAnimate();
        isWalk = false;

        ClickScope();
    }
    void ClickScope(){
        if (Input.GetMouseButtonDown(1)){
            scope.SetActive(true);
            CanShoot = true;
      
        }
        else if (Input.GetMouseButtonUp(1)){
            scope.SetActive(false);
            Camera.main.fieldOfView = 60f;
            CanShoot = false;
        }
        if(Input.GetMouseButton(0) && CanShoot){
            if (BulletCout>0){
                Shoot();
            }
              BulletCout -=1;
            }
        if (Input.mouseScrollDelta.y != 0 && CanShoot){
                float scroll = Input.mouseScrollDelta.y;
                Camera.main.fieldOfView += -scroll * zoomSpeed * Time.deltaTime;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20f, 60f);
            }
     
    }
    void Shoot(){
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 2f; 
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 cameraDirection = Camera.main.transform.forward;
            spawnPosition.y += cameraDirection.y-0.75f;
            Instantiate(bullet, spawnPosition, pointBullet.rotation);

    }
    IEnumerator wait(float Delay,bool check, GameObject obj ){
        yield return new WaitForSeconds(Delay);
        obj.SetActive(check);
    }
    void RunAnimate(){
        if (isWalk){
            animate.SetBool("isWalking",true);
        }
        else{
            animate.SetBool("isWalking", false);
        }
    }
    
}
