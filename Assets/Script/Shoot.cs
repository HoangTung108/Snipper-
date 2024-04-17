using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody rb;
    public float speed ;
    
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= 1f;
        Movement.isCollide = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraForward = Camera.main.transform.forward;
        if (Camera.main.fieldOfView >40 && Camera.main.fieldOfView <60){
            CameraForward.y +=0.1f;
        }
        else if (Camera.main.fieldOfView >=10 && Camera.main.fieldOfView <=40){
            CameraForward.y +=0.08f;
        }
        else{
            CameraForward.y +=0.12f;
        }
        CameraForward.Normalize();
        rb.AddForce(CameraForward*speed, ForceMode.Impulse);
        // rb.velocity = CameraForward*speed *Time.deltaTime;
    }
    void OnCollisionEnter(Collision other ){
        if (other.gameObject.tag == "Enemy"){
            
            StartCoroutine(destroy(other.gameObject,0.75f));

            StartCoroutine(destroy(gameObject,1f));
            
        }
    
            Movement.isCollide = true;
            StartCoroutine(destroy(gameObject,1f));
            
        
    }
    IEnumerator destroy(GameObject obj,float delay){
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
