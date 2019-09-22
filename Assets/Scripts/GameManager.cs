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
    private Turn turn = Turn.HUMAN;

    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject HumanPlayer;
    [SerializeField] private GameObject RaccoonPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchTurn() {
        turn = (turn == Turn.HUMAN) ? Turn.RACCOON : Turn.HUMAN;
        CameraSwitch();
    }

    private void CameraSwitch() {
        
    }

    //TODO
    //Camera switch on turn switch
    //Check win
    //Turn coroutine

    // private IEnumerator GameLoop()
    // {
    //     yield return StartCoroutine(TurnProgress());

    //     if (m_GameWinner != null)
    //     {
    //         //reset
    //     }
    //     else
    //     {
    //         StartCoroutine(GameLoop());
    //     }
    // }

    // private IEnumerator TurnProgress() {
    //     if (turn == Turn.HUMAN){
    //         //check walk and interact
    //         //switch
    //     }
    //     else {
    //         //check walk
    //         //switch
    //     }
    // }
}
