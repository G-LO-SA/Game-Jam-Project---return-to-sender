using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{

    private float elapsedtime = 0f;
    private float timed = 0f;
    [HideInInspector] public float killed = 0f;
    public Text uiText;
    public Text uiTextP;
    public Text uiTextGameover;
    public Text uiTextPGameover;


    void FixedUpdate()
    {
        if (elapsedtime < 60f)
        {
            elapsedtime += Time.deltaTime;
        }

        else
        {
            elapsedtime = 0f;
            timed += 1f;
        }
        uiText.text = "Timer: " + timed.ToString("F0") + ":" + elapsedtime.ToString("F0");

        uiTextP.text = "Killed: " + killed.ToString();

        gameover();
    }

    private void gameover()
    {
        uiTextGameover.text = "Time Survived: " + timed.ToString("F0") + ":" + elapsedtime.ToString("F0") ;
        uiTextPGameover.text = "Kills: " + killed.ToString();
    }


}
