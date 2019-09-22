using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private bool ZoomToggle;
    private Camera myCamera;
    private float toggle;
    public float dampTime = 0.2f;
    public Vector3 moveVelocity;

    
    void Awake(){
        ZoomToggle = false;
        myCamera = GetComponentInChildren<Camera>();
        moveVelocity = new Vector3(1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        toggle = Input.GetAxis("CameraToggle");
    }

    void FixedUpdate()
    {
        if(Input.GetKey("z")){
            StartCoroutine(CameraZoom());
        }
        if(Input.GetKeyUp("z")){
            StartCoroutine(CameraReset());
        }
    }

    IEnumerator CameraZoom(){
        myCamera.transform.position = Vector3.SmoothDamp(myCamera.transform.position, 
            new Vector3(0,60,-35), ref moveVelocity, dampTime);
        myCamera.transform.rotation = Quaternion.Euler(65,0,0);
        yield return new WaitForSeconds(0.5f);
        ZoomToggle = true;
        while(toggle > 0){
            yield return null;
        }
    }

    IEnumerator CameraReset(){
        myCamera.transform.position = transform.position + new Vector3(0,12,-7);
        //myCamera.transform.rotation = Quaternion.Euler(65,0,0);
        yield return new WaitForSeconds(0.5f);
        ZoomToggle = false;
        while(myCamera.transform.position != transform.position + new Vector3(0,10,0)){
            yield return null;
        } 
    }
}
