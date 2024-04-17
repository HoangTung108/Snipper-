using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public static bool isCollide;
    public float speed;
    public Animator animate;
    public GameObject scope;
    public GameObject gun;
    public GameObject bullet;
    public GameObject LoseUI;
    public static GameObject WINUI;
    public GameObject PointUI;
    public Transform pointBullet;
    public List <GameObject> listobj;
    public List <GameObject> listNPC;
    public Text text;
    public float zoomSpeed;
    private int BulletCout;
    private bool isWalk;
    private bool CanShoot;
    private bool GunShoot;
    private  Vector3 cameraForward ;
    private  Vector3 cameraRight ;
    public Rigidbody rb;
    private  string[] listContent =  {"Listen here 319 !!!" ,"I have an extremely important mission.", 
    "In just a few minutes, the presidential candidate's car will arrive.", 
    "I can only stand on this high-rise building and aim at his position.",
    "You only have one bullet to finish him off,",
    "Use it accurately to complete this mission before he participates in the election,",
     "Good luck !!!"};
     private  int i =0;
    private bool nextText;
    private bool CanDo;
 

    void Awake(){  
        HideNPC(listobj, false);
        HideNPC(listNPC, false);
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= 10f;
        text.text = string.Empty;
        CanDo =false;
       
    }

    void Start(){
        StartCoroutine(Show(listContent[0]));
         BulletCout =1;
    }

    // Update is called once per frame
    void Update()
    {   
        if (CanDo){
            ControlMovement();
        }
        ShowText();
        Lose();
        
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
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 10f, 60f);
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
    void RunAnimate(){
        if (isWalk){
            animate.SetBool("isWalking",true);
        }
        else{
            animate.SetBool("isWalking", false);
        }
    }
    void HideNPC(List<GameObject> lst, bool check){
        foreach (GameObject i in lst){
            i.SetActive(check);
        }
    }
    void ShowText(){
       
        if (Input.GetMouseButtonDown(0) && nextText){
            i +=1;

            text.text = string.Empty;
            if (i < listContent.Length){
                StartCoroutine(Show(listContent[i]));
            }
            else{
                CanDo = true;
                HideNPC(listobj, true);
                foreach (GameObject t in listNPC){
                     StartCoroutine(wait(8f,true,t) );
                }
               
            }
           nextText = false;
        }  
    }
    void Lose(){
        if (isCollide && BulletCout <1){
            LoseUI.SetActive(true);
            PointUI.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale =0f;
            
        }
    }
    IEnumerator wait(float Delay,bool check, GameObject obj ){
        yield return new WaitForSeconds(Delay);
        obj.SetActive(check);
    }
    IEnumerator Show (string content){
        foreach (char Char in content.ToCharArray()){
            text.text += Char;
            yield return new WaitForSeconds(0.1f);
        }
        nextText =true;
    }
    
}
