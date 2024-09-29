using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeAuto : MonoBehaviour
{
    public GameObject eyes;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Animator anim  = GetComponent<Animator>();
        StartCoroutine(GameChange());
    }

    IEnumerator GameChange()
    {
        yield return new WaitForSeconds(42f);
        eyes.SetActive(true);
        SceneManager.LoadScene(2);
    }
}
