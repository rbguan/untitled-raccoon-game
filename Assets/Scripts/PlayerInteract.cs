using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [HideInInspector] public bool DoneAction;
    private Rigidbody myRigidBody;
    private SphereCollider myReach;
    public LayerMask Interactables;
    private float InteractInput;
    
    // Start is called before the first frame update
    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myReach = GetComponent<SphereCollider>();
        DoneAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        InteractInput = Input.GetAxis("Fire1");
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, myReach.radius, Interactables);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(InteractInput > 0){

            }
        }
    }


}
