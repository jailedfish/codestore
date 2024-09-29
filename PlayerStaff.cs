using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class PlayerStaff : MonoBehaviour {
    public AudioClip deathSound; 
    GameObject oldParent;

    public IEnumerator OnDeath() {
        GameObject endScreen = GameObject.Find("/Canvas/endScreen");
        oldParent = gameObject.transform.parent.gameObject;
//        camera.transform.parent = null;
//        camera.GetOrAddComponent<AudioListener>();
//        camera.GetOrAddComponent<AudioSource>().PlayOneShot(deathSound);
        yield return new WaitForSeconds(0.1f);
        Destroy(oldParent);
        endScreen.GetComponent<VideoPlayer>().enabled = true;
        endScreen.GetComponent<VideoPlayer>().targetCamera = gameObject.GetComponent<Camera>();
        endScreen.SetActive(true);
        
    }
}
