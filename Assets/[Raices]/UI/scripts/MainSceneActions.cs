using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneActions : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level-0");
    }
}
