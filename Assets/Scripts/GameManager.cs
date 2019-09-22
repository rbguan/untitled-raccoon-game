using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private PlayerMovement HumanMovement;
    private PlayerMovement RaccoonMovement;
    private Canvas HumanCanvas;
    private Canvas RaccoonCanvas;

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
        HumanMovement = HumanPlayer.GetComponentInChildren<PlayerMovement>();
        RaccoonMovement = RaccoonPlayer.GetComponentInChildren<PlayerMovement>();
        HumanCamera = HumanPlayer.GetComponentInChildren<Camera>();
        RaccoonCamera = RaccoonPlayer.GetComponentInChildren<Camera>();
        HumanCanvas = HumanPlayer.GetComponentInChildren<Canvas>();
        RaccoonCanvas = RaccoonPlayer.GetComponentInChildren<Canvas>();
    }

    void Start(){
        ControlInit();
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
        if(RaccoonCamera != null){RaccoonCamera.enabled = false;}
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
        HumanMovement.myTurn = true;
        HumanCanvas.enabled = true;
        HumanPlayer.GetComponentInChildren<AudioSource>().Play();
    }

    private void HumanDisable() {
        HumanPlayer.GetComponent<PlayerInteract>().CanInteract = false;
        HumanCanvas.enabled = false;
        HumanMovement.myTurn = false;
    }

    private void RaccoonEnable() {
        RaccoonCanvas.enabled = true;
        RaccoonMovement.myTurn = true;
        RaccoonPlayer.GetComponentInChildren<AudioSource>().Play();
    }

    private void RaccoonDisable() {
        if(RaccoonCanvas != null) {RaccoonCanvas.enabled = false;}
        if(RaccoonCanvas != null){RaccoonMovement.myTurn = false;}
    }

    private IEnumerator GameLoop()
    {
        HumanEnable();
        RaccoonDisable();
        yield return StartCoroutine(HumanTurn());
        if (CheckWin())
        {
            //CURRENT: RELOAD SCENE
            //TODO: GO TO ENDING SCENES
            SceneManager.LoadScene("MainGame");
        }

        yield return StartCoroutine(InBetweenTurns());
        HumanDisable();
        HumanMovement.TurnReset();
        SwitchTurn();
        RaccoonEnable();
        HumanDisable();
        yield return StartCoroutine(RaccoonTurn());
        if (CheckWin())
        {
            //CURRENT: RELOAD SCENE
            //TODO: GO TO ENDING SCENES
            SceneManager.LoadScene("MainGame");
        }
        yield return StartCoroutine(InBetweenTurns());
        RaccoonDisable();
        RaccoonMovement.TurnReset();
        SwitchTurn();
        if (CheckWin())
        {
            //CURRENT: RELOAD SCENE
            //TODO: GO TO ENDING SCENES
            SceneManager.LoadScene("MainGame");
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private void HumanMeterReset(){
        HumanPlayer.GetComponent<PlayerMovement>().TurnReset();
    }

    private IEnumerator HumanTurn(){

        while(HumanPlayer.GetComponent<PlayerMovement>().WalkRemaining > 0 
            || HumanPlayer.GetComponent<PlayerInteract>().DoneAction != true && !HumanPlayer.GetComponent<PlayerInteract>().HumanWin){
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
        if (!HumanPlayer.GetComponent<PlayerInteract>().HumanWin &&
            !HumanPlayer.GetComponent<PlayerInteract>().RaccoonWin)
        {
            return false;
        } 
        return true;
    }
}
