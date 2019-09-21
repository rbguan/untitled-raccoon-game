using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float InitialWalkRange;
    [SerializeField] private float ParanoiaLevel;
    [SerializeField] private float WalkRemaining;
    [SerializeField] private bool DoneAction;
    [SerializeField] private float WalkSpeed;
    private Rigidbody myRigidBody;
    private float VerticalInput;
    private float HorizontalInput;

    void Awake()
    {
        // InitialWalkRange = 30.0f;
        // ParanoiaLevel = 1.0f;
        WalkRemaining = InitialWalkRange * ParanoiaLevel;
        // DoneAction = false;
        myRigidBody = GetComponent<Rigidbody>();
        // WalkSpeed = 5.0f;

    }
    // Update is called once per frame
    void Update()
    {
        VerticalInput = Input.GetAxis("Vertical");
        HorizontalInput = Input.GetAxis("Horizontal");
        
    }

    void FixedUpdate()
    {
        if(WalkRemaining > 0)
        {
            Vector3 moveVector = transform.forward * VerticalInput * WalkSpeed * Time.deltaTime;
            myRigidBody.MovePosition(myRigidBody.position + moveVector);

            Vector3 sideVector = transform.right * HorizontalInput * WalkSpeed * Time.deltaTime;
            myRigidBody.MovePosition(myRigidBody.position + sideVector);

            WalkRemaining -= (sideVector.magnitude + moveVector.magnitude);
        }
        
        
    }

}
