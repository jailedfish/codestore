using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutScene : MonoBehaviour
{
    public GameObject cutSceneObject;
    public GameObject loadingText;
    public GameObject eyes;
    public Animator anim;
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        eyes.SetActive(true);
        loadingText.SetActive(true);
        StartCoroutine(startCutScene());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator startCutScene()
    {   
        yield return new WaitForSeconds(2f);
        loadingText.SetActive(false);
        eyes.SetActive(false);
        cutSceneObject.SetActive(true);
    }
}
