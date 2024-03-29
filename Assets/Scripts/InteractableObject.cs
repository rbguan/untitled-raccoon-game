﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class InteractableObject : MonoBehaviour
{
    SphereCollider myCollider;
    public Light spotlight;
    private Light myLight;
    // Start is called before the first frame update
    virtual public void Awake()
    {
        Debug.Log("superclass");
        Light spotlightInstance = Instantiate(spotlight, new Vector3(0, 7, 0) + transform.position, 
            Quaternion.identity, gameObject.transform) as Light;
        spotlightInstance.transform.rotation = Quaternion.Euler(90, 0, 0);
        myLight = spotlightInstance;
        myLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DoAction(){
        myLight.color = Color.red;
        if(gameObject.tag == "Finish"){
            SceneManager.LoadScene("MainGame");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        myLight.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        myLight.enabled = false;
    }
    
}
