using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupMenu : MonoBehaviour
{
    public void ShowStartScreen () {
        SceneManager.LoadScene(0);
    }

    /* Not working: Try to switch to scene "How To Play" without clearing the MainGame scene */
    public void ShowHowToPlay() {
        Scene mainGameScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("How To Play", LoadSceneMode.Additive);
    }
}
