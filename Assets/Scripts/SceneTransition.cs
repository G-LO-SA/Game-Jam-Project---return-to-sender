using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
   public void transitionGameScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void transitionMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
