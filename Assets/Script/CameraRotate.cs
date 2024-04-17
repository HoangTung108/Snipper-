using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform pointShoot;
    public Transform player;
    public float SenX;
    public float SenY;
    private float MouseX;
    private float MouseY;
    private float xRotation;
    private float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
    }
    void CameraControl(){
        MouseX = Input.GetAxis("Mouse X") * SenX * Time.deltaTime;
        MouseY = Input.GetAxis ("Mouse Y") * SenY * Time.deltaTime;
        yRotation += MouseX;
        xRotation -=MouseY;
        xRotation = Mathf.Clamp (xRotation , -60f, 30f);
        transform.rotation = Quaternion.Euler (xRotation,yRotation,0);
        player.rotation = Quaternion.Euler (0,yRotation,0);
        pointShoot.rotation = Quaternion.Euler(0,yRotation,0);
    }
}
