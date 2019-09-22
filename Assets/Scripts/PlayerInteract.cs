using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [HideInInspector] public bool DoneAction;
    [HideInInspector] public bool CanInteract;
    [HideInInspector] public bool RaccoonWin;
    [HideInInspector] public bool HumanWin;

private Rigidbody myRigidBody;
    private SphereCollider myReach;
    public LayerMask Interactables;
    public LayerMask StreetLights;
    public LayerMask Finish;
    public LayerMask Raccoon;
    private float InteractInput;
    [SerializeField] private PlayerMovement myMovement;
    
    // Start is called before the first frame update
    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myReach = GetComponent<SphereCollider>();
        CanInteract = true;
        DoneAction = false;
        RaccoonWin = false;
        HumanWin = false;
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
        Collider[] lightColliders = Physics.OverlapSphere(transform.position, myReach.radius, StreetLights);
        if(lightColliders.Length > 0){
            myMovement.touchedLight = true;
        }

        Collider[] raccoonColliders = Physics.OverlapSphere(transform.position, myReach.radius, Raccoon);
        Collider[] finishColliders = Physics.OverlapSphere(transform.position, myReach.radius, Finish);

        if (raccoonColliders.Length > 0)
        {
            RaccoonWin = true;
        }

        if (finishColliders.Length > 0)
        {
            HumanWin = true;
        }
    }


}
