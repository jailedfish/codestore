using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseUI;
    public bool IsPaused = false;
    public GameObject PausingCamera;
    public GameObject ExitUI;
    public GameObject gamecamera;
    public Animator pauseanimator;
    public GameObject[] disablingitems;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
        {
            if (IsPaused == false)
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        PauseUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        IsPaused = true;
        PausingCamera.SetActive(true);
        gamecamera.SetActive(false);
        StartCoroutine(PauseWaiting());
        foreach (GameObject obj in disablingitems)
        {
            obj.SetActive(false);
        }
    }
    IEnumerator PauseWaiting()
    {
        yield return new WaitForSeconds(0.60f);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1.0f;
        PauseUI.SetActive(false);
        gamecamera.SetActive(true);
        PausingCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        foreach (GameObject obj in disablingitems)
        {
            obj.SetActive(true);
        }
    }
    public void ExitInGameWarning()
    {
        PauseUI.SetActive(false);
        ExitUI.SetActive(true);
    }
    public void CancelExitInGame()
    {
        Time.timeScale = 1.0f;
        PauseUI.SetActive(true);
        ExitUI.SetActive(false);
        StartCoroutine(PauseWaiting());
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
