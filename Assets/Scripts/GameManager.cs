using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject playermove;
   
    private void Update()
    {
        gameover();
    }
    private void gameover()
    {
        PlayerMove player = playermove.GetComponent<PlayerMove>();
        float playerhealth = player.currenthealth;

        if (playerhealth == 0f) 
        {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        }

        else
        {
            gameOverScreen.SetActive(false);
        }
        
    }

    public void pause(int status)
    {
        Time.timeScale = status;
    }
}
