using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /* Switch to the MainGame scene by moving to the next scene in the queue. 
    The scene queue can be found in File > Build Settings */
    public void PlayGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /* Switch to scene "How To Play" by name */
    public void ShowHowToPlay() {
        SceneManager.LoadScene("How To Play");
    }

    /* Switch to scene "How To Play" by name */
    public void ShowCredits() {
        SceneManager.LoadScene("Credits");
    }
}
