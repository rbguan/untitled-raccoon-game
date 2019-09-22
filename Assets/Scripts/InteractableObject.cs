using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class InteractableObject : MonoBehaviour
{
    SphereCollider myCollider;
    public Light spotlight;
    // Start is called before the first frame update
    void Awake()
    {
        Light spotlightInstance = Instantiate(spotlight, new Vector3(0, 7, 0) + transform.position, new Quaternion(90,0,0,0)) as Light;
        spotlightInstance.transform.parent = gameObject.transform;
        spotlight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAction(){
        spotlight.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        spotlight.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        spotlight.enabled = false;
    }
    
}
