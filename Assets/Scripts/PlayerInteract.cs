﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [HideInInspector] public bool DoneAction;
    [HideInInspector] public bool CanInteract;
    private Rigidbody myRigidBody;
    private SphereCollider myReach;
    public LayerMask Interactables;
    public LayerMask StreetLights;
    private float InteractInput;
    [SerializeField] private PlayerMovement myMovement;
    
    // Start is called before the first frame update
    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myReach = GetComponent<SphereCollider>();
        CanInteract = true;
        DoneAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        InteractInput = Input.GetAxis("Fire1");
    }


    private void OnTriggerStay(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, myReach.radius, Interactables);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(CanInteract && InteractInput > 0 && !DoneAction){
                colliders[i].GetComponentInParent<InteractableObject>().DoAction();
                DoneAction = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, myReach.radius, StreetLights);
        if(colliders.Length > 0){
            myMovement.touchedLight = true;
        }
    }


}
