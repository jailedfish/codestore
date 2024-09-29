using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tester : MonoBehaviour
{
    public Button save, load;
    public Text data;
    public GameObject player;
    private Player _info;
    Player plr;
    void Start() {
        save.onClick.AddListener(() => SavesManager.SaveGame(plr));
        load.onClick.AddListener(() =>
        {
            data.enabled = true;
            _info = SavesManager.LoadGame();
            player.GetComponent<Transform>().position = _info.pos;
            player.GetComponent<HealthManager>().health = _info.health;
            
        });
    }

    // Update is called once per frame
    void Update() {
        plr = new Player(player.GetComponent<Transform>().position, player.GetComponent<HealthManager>());
        plr.health = 5;
    }
}
