using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    /* TODO: make this more reusable by keeping track of previous scene */ 
    public void GoToPreviousScene () {
        SceneManager.LoadScene(0); // switches to the start screen
    }
}
