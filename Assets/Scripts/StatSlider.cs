using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSlider : MonoBehaviour
{
    public float StartingLevel;
    public Slider mySlider;
    public Image fillImage;

    private void Awake()
    {
        mySlider.minValue = 0;
    }

    private void Start()
    {
        mySlider.value = StartingLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel(float change)
    {
        mySlider.value += change;
    }
}
