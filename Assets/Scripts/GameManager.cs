using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private enum Turn {
        HUMAN,
        RACCOON
    }

    // Whose turn it is
    private Turn turn;

    [SerializeField] private Camera MainCamera;
    [SerializeField] private GameObject HumanPlayer;
    [SerializeField] private GameObject RaccoonPlayer;
    public Transform HumanSpawnPoint;
    public Transform RaccoonSpawnPoint;
    public float timeBetweenTurns = 4f;
    private WaitForSeconds waitTime;

    // Start is called before the first frame update
    void Start()
    {
        turn = Turn.HUMAN;
        //attach camera to human
        ControlInit();
        CameraInit();
        waitTime = new WaitForSeconds(timeBetweenTurns);
        StartCoroutine(GameLoop());
    }


    private void SwitchTurn() {
        turn = (turn == Turn.HUMAN) ? Turn.RACCOON : Turn.HUMAN;
        ControlSwitch();
        CameraSwitch();
    }

    private void ControlInit() {
        HumanEnable();
        RaccoonDisable();
    }

    private void ControlSwitch() {
        if (turn == Turn.HUMAN){
            HumanEnable();
            RaccoonDisable();
        }
        else {
            HumanDisable();
            RaccoonEnable();
        }
    }

    private void CameraInit() {
        MainCamera.transform.parent = HumanPlayer.transform;
    }

    private void CameraSwitch() {
        if (turn == Turn.HUMAN){
            MainCamera.transform.parent = HumanPlayer.transform;
        } else {
            MainCamera.transform.parent = RaccoonPlayer.transform;
        }
    }

    private void HumanEnable() {
        HumanPlayer.GetComponent<PlayerInteract>().CanInteract = true;
        HumanPlayer.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void HumanDisable() {
        HumanPlayer.GetComponent<PlayerInteract>().CanInteract = false;
        HumanPlayer.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void RaccoonEnable() {
        RaccoonPlayer.GetComponent<PlayerInteract>().CanInteract = true;
        RaccoonPlayer.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void RaccoonDisable() {
        RaccoonPlayer.GetComponent<PlayerInteract>().CanInteract = false;
        RaccoonPlayer.GetComponent<Rigidbody>().isKinematic = true;
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(HumanTurn());
        HumanDisable();
        if (CheckWin())
        {
            //reset
        }
        SwitchTurn();
        yield return StartCoroutine(InBetweenTurns());

        yield return StartCoroutine(RaccoonTurn());

        RaccoonDisable();
        SwitchTurn();
        
        yield return StartCoroutine(InBetweenTurns());

        if (CheckWin())
        {
            //reset
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    // private IEnumerator TurnProgress() {
    //     if (turn == Turn.HUMAN){
    //         if(HumanPlayer.GetComponent<PlayerMovement>().WalkRemaining <= 0 
    //         && HumanPlayer.GetComponent<PlayerInteract>().DoneAction == true){
    //             SwitchTurn();
    //         }
    //     }
    //     else {
    //         if(RaccoonPlayer.GetComponent<PlayerMovement>().WalkRemaining <= 0){
    //             SwitchTurn();
    //         }
    //     }
    //     yield return null;
    // }

    private IEnumerator HumanTurn(){
        HumanEnable();
        while(HumanPlayer.GetComponent<PlayerMovement>().WalkRemaining > 0 
            && HumanPlayer.GetComponent<PlayerInteract>().DoneAction != true){
                yield return null;
            }
    }

    private IEnumerator RaccoonTurn(){
        RaccoonEnable();
        while(RaccoonPlayer.GetComponent<PlayerMovement>().WalkRemaining > 0){
                yield return null;
            }
    }

    private IEnumerator InBetweenTurns() {
        yield return new WaitForSeconds(3.0f);
    }

    private bool CheckWin(){
        //check human win
        //check raccoon win
        //return
        return false;
    }
}
