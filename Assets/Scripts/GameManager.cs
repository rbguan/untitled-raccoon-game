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

    [SerializeField] private Camera HumanCamera;
    [SerializeField] private Camera RaccoonCamera;
    [SerializeField] private GameObject HumanPlayer;
    [SerializeField] private GameObject RaccoonPlayer;
    public Transform HumanSpawnPoint;
    public Transform RaccoonSpawnPoint;
    public float timeBetweenTurns = 4f;
    private WaitForSeconds waitTime;

    // Start is called before the first frame update
    void Awake()
    {
        turn = Turn.HUMAN;
        //attach camera to human
        waitTime = new WaitForSeconds(timeBetweenTurns);
        HumanPlayer = GameObject.FindGameObjectWithTag("Human");
        RaccoonPlayer = GameObject.FindGameObjectWithTag("Raccoon");
        HumanSpawnPoint = GameObject.FindGameObjectWithTag("HumanSpawn").transform;
        RaccoonSpawnPoint = GameObject.FindGameObjectWithTag("RaccoonSpawn").transform;
        ControlInit();
        HumanCamera = HumanPlayer.GetComponentInChildren<Camera>();
        RaccoonCamera = RaccoonPlayer.GetComponentInChildren<Camera>();
        CameraInit();
        HumanPlayer.transform.position = HumanSpawnPoint.position;
        RaccoonPlayer.transform.position = RaccoonSpawnPoint.position;
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
        HumanCamera.enabled = true;
        RaccoonCamera.enabled = false;
    }

    private void CameraSwitch() {
        if (turn == Turn.HUMAN){
            HumanCamera.enabled = true;
            RaccoonCamera.enabled = false;
        } else {
            HumanCamera.enabled = false;
            RaccoonCamera.enabled = true;
        }
    }

    private void HumanEnable()
    {
        HumanPlayer.GetComponent<PlayerInteract>().CanInteract = true;
        HumanPlayer.GetComponent<Rigidbody>().isKinematic = false;
        HumanPlayer.GetComponentInChildren<AudioSource>().Play();
    }

    private void HumanDisable() {
        HumanPlayer.GetComponent<PlayerInteract>().CanInteract = false;
        HumanPlayer.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void RaccoonEnable() {
        RaccoonPlayer.GetComponent<Rigidbody>().isKinematic = false;
        RaccoonPlayer.GetComponentInChildren<AudioSource>().Play();
    }

    private void RaccoonDisable() {
        RaccoonPlayer.GetComponent<Rigidbody>().isKinematic = true;
    }

    private IEnumerator GameLoop()
    {
        HumanEnable();
        RaccoonDisable();
        yield return StartCoroutine(HumanTurn());
        HumanDisable();

        yield return StartCoroutine(InBetweenTurns());
        SwitchTurn();
        RaccoonEnable();
        HumanDisable();
        yield return StartCoroutine(RaccoonTurn());

        RaccoonDisable();
        yield return StartCoroutine(InBetweenTurns());
        SwitchTurn();
        if (CheckWin())
        {
            //reset
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator HumanTurn(){

        while(HumanPlayer.GetComponent<PlayerMovement>().WalkRemaining > 0 
            || HumanPlayer.GetComponent<PlayerInteract>().DoneAction != true){
                yield return null;
            }
    }

    private IEnumerator RaccoonTurn(){
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
