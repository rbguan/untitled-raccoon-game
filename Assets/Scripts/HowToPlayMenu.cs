using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayMenu : MonoBehaviour
{
    public GameObject[] pages;
    public int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        pages[0].SetActive(true);
        for(int i = 1; i < pages.Length; i++) {
            pages[i].SetActive(false);
        }
    }

    public void Forward() {
        if(current < pages.Length -1) {
            pages[current].SetActive(false);
            current++;
            pages[current].SetActive(true);
        }
    }

    public void Back() {
        if(current > 0) {
            pages[current].SetActive(false);
            current--;
            pages[current].SetActive(true);
        }
    }
}
