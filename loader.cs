using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loader : MonoBehaviour {
    public Button load;
    private Player _player;
    void Start() {
        load.onClick.AddListener(() => StartCoroutine(LoadGame()));
    }

    private void Update() {
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadGame() {
        SceneManager.LoadScene("Game");
        
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Game");
        _player = SavesManager.LoadGame();
        GameObject player = GameObject.Find("/Player");
        player.GetComponent<Transform>().position = _player.pos;
        player.GetComponent<HealthManager>().health = _player.health;
    }
}
