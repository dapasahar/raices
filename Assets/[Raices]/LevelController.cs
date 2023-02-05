using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    static LevelController instance;
    public static LevelController Instance { get { return instance; } }

    private void Awake()
    {
        instance = this; 
        winCanvas.gameObject.SetActive(false);
    }

    RootController rootController;

    public float startTime = 3f;

    public float transitionTime = 3f;

    public CinemachineVirtualCamera startCamera;

    public CinemachineVirtualCamera gameCamera;

    public Canvas winCanvas;

    IEnumerator Start()
    {
        rootController = FindObjectOfType<RootController>();
        gameCamera.enabled = false;

        yield return new WaitForSeconds(startTime);

        gameCamera.enabled = true;

        yield return new WaitForSeconds(transitionTime);

        rootController.run = true;
    }

    public void Victoria()
    {
        Debug.Log("V");
        winCanvas.gameObject.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
