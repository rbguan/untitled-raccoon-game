using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float InitialWalkRange;
    [SerializeField] private float ParanoiaLevel;
    [HideInInspector] public float WalkRemaining;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float TurnSpeed;
    private Rigidbody myRigidBody;
    private float VerticalInput;
    private float HorizontalInput;
    private StatSlider myWalkSlider;
    private StatSlider myParanoiaSlider;   


    void Awake()
    {
        WalkRemaining = InitialWalkRange * ParanoiaLevel;
        myRigidBody = GetComponent<Rigidbody>();
        myWalkSlider = GetComponent<StatSlider>();
        myWalkSlider.StartingLevel = WalkRemaining;
        myWalkSlider.mySlider.maxValue = WalkRemaining;
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
            float turn = HorizontalInput * TurnSpeed * Time.deltaTime;
            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
            // Apply this rotation to the rigidbody's rotation.
            myRigidBody.MoveRotation(myRigidBody.rotation * turnRotation);
            WalkRemaining -= (moveVector.magnitude);
            myWalkSlider.mySlider.value -= moveVector.magnitude;
        }
    }

}
