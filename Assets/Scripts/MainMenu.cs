using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{

    public GameObject credits;
    private void Start()
    {
        AudioManager.instance.PlayBgm(1);
    }
 public void PlayGame ()
 {
   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
 }

 public void QuitGame ()
 {
   Debug. Log ("GAME Quit");
  Application.Quit();
 }

    public void BackToMainMenu()
    {
        credits.SetActive(false);
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

}
