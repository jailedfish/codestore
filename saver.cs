using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class saver : MonoBehaviour {
    private Player player;
    public GameObject plr;
    public Text text;
    void Start() {
        text.text = "Your position was saved";
        text.enabled = false;
        StartCoroutine(SaveCoroutine());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        player = new Player(plr.GetComponent<Transform>().position, plr.GetComponent<HealthManager>());

        if (Input.GetKeyDown(KeyCode.F5))
        {
            StartCoroutine(SaveGame());
        }
    }

    IEnumerator SaveGame() {
        SavesManager.SaveGame(player);
        text.enabled = true;
        for (int x = 0; x < 32; x++)
        {
            text.GetComponent<RectTransform>().Translate(new Vector3(0, -1f));
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(5f);
        text.enabled = false;
        text.GetComponent<RectTransform>().Translate(new Vector3(0, 32));
    }
    
    IEnumerator SaveCoroutine() {
        while (true)
        {
            yield return new WaitForSeconds(60 * 3f);
            StartCoroutine(SaveGame());
        }
    }
}
