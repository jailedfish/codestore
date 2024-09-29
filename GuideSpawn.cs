using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GuideSpawn : MonoBehaviour
{
    public GameObject Player;
    public GameObject Eyes;
    public GameObject Text;

    public AudioSource audioSource;

    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    public AudioClip audioClip4;

    private void Start()
    {
        Player.GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine(ScrenePlayer ());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Eyes.SetActive(true);
        
    }

    IEnumerator ScrenePlayer() {
        yield return new WaitForSeconds(1.5f);
        Eyes.SetActive(false);
        Text.GetComponent<Text>().text = "Then I woke up here";
        audioSource.PlayOneShot(audioClip1);
        yield return new WaitForSeconds(audioClip1.length);
        Text.GetComponent<Text>().text = "What happened? How did I end up here?";
        audioSource.PlayOneShot(audioClip2);
        yield return new WaitForSeconds(audioClip2.length);
        Text.GetComponent<Text>().text = "Okay, in any case, you can't stay in place";
        audioSource.PlayOneShot(audioClip3);
        yield return new WaitForSeconds(audioClip3.length);
        Text.GetComponent<Text>().text = "We need to search everything here";
        audioSource.PlayOneShot(audioClip4);
        yield return new WaitForSeconds(audioClip4.length);
        Text.GetComponent<Text>().text = "";
        Player.GetComponent<FirstPersonController>().enabled = true;
        SceneManager.LoadScene("Game");
    }

}
