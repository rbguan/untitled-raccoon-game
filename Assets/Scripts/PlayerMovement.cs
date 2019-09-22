using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float InitialWalkRange;
    [SerializeField] private float ParanoiaLevel = 1f;
    public float WalkRemaining;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float TurnSpeed;

    private Rigidbody myRigidBody;
    private float VerticalInput;
    private float HorizontalInput;
    [SerializeField] public StatSlider myWalkSlider;
    [SerializeField] public StatSlider myParanoiaSlider;   
    [HideInInspector] public bool touchedLight;
    [HideInInspector] public bool edittedParanoia;
    [SerializeField] private LayerMask InteractCheck;
    [HideInInspector] public bool myTurn;
    void Awake()
    {
        touchedLight = false;
        edittedParanoia = false;
        WalkRemaining = InitialWalkRange * ParanoiaLevel;
        myRigidBody = GetComponent<Rigidbody>();
        myWalkSlider.StartingLevel = WalkRemaining;
        myWalkSlider.mySlider.maxValue = WalkRemaining;
        if(gameObject.tag != "Raccoon"){myParanoiaSlider.StartingLevel = 1f;}
    }
    // Update is called once per frame
    void Update()
    {
        if(myTurn){
            VerticalInput = Input.GetAxis("Vertical");
            HorizontalInput = Input.GetAxis("Horizontal");
        }

        
    }

    void FixedUpdate()
    {
        if(myTurn){
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
                myWalkSlider.mySlider.value = (myWalkSlider.mySlider.maxValue * (WalkRemaining / (InitialWalkRange * ParanoiaLevel)));
            } else{
                if(gameObject.tag != "Raccoon"){
                    if(!edittedParanoia){
                        if(touchedLight){
                            if(myParanoiaSlider.mySlider.value <= 1){
                                myParanoiaSlider.mySlider.value = 1;
                                ParanoiaLevel = 1;
                            } else{
                                myParanoiaSlider.mySlider.value--;
                                ParanoiaLevel -= 1f;
                            }
                            touchedLight = false;
                            edittedParanoia = true;
                        } else{
                            if(myParanoiaSlider.mySlider.value >= 4){
                                myParanoiaSlider.mySlider.value = 4;
                                ParanoiaLevel = 4;
                            } else{
                                myParanoiaSlider.mySlider.value++;
                                ParanoiaLevel += 1f;
                            }
                            edittedParanoia = true;
                        }
                    }
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, InteractCheck);
                    if(colliders.Length < 1){
                        GetComponentInParent<PlayerInteract>().DoneAction = true;
                    }
                }
                
            }
        }
        
    }

    public void TurnReset(){
        WalkRemaining = ParanoiaLevel * InitialWalkRange;
        touchedLight = false;
        edittedParanoia = false;
        myWalkSlider.mySlider.value = myWalkSlider.mySlider.maxValue;
        if(gameObject.tag == "Human"){GetComponentInParent<PlayerInteract>().DoneAction = false;}
    }

}
