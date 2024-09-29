using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject eyeClose;
    public GameObject loadText;
    public AudioClip buttonClip;
    public AudioSource buttonSound;
    public GameObject PanelStart;

    public void StartGames()
    {
        StartCoroutine(NewGameStart());
    }

    IEnumerator NewGameStart()
    {
        eyeClose.SetActive(true);
        PanelStart.SetActive(false);
        buttonSound.PlayOneShot(buttonClip);
        loadText.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
