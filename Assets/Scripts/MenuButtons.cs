using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MenuButtons : MonoBehaviour
{
    public void quit()
    {
        Debug.Log("quitting");
        Application.Quit();
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }
}
