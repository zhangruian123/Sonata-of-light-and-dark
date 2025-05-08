using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eventController : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene0.5");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
