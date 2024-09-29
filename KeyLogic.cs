using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyLogic : MonoBehaviour {
    private Dictionary<int, Key> 
        keys = new Dictionary<int, Key>(), 
        fkeys = new Dictionary<int, Key>();
    
    public Text text;
    public float distance;

    public void Start() {
        int i = 0;
        foreach (GameObject key in GameObject.FindGameObjectsWithTag("Key")) {
            keys.Add(i, new Key(key, key.GetComponent<CustomInfo>().CustomInfoMain, key.GetComponent<CustomInfo>().tags));
            i++;
        }
        fkeys = keys;
    }

    public bool HaveKey(string tag) {
        if (tag == "")
        {
            return true;
        }
        return keys.Values.Where((key) => (key.CanOpen.Contains(tag) && key.PickedUp)).Count() > 0;
    }
    
    private IEnumerator displaymessage(string msg) {
        text.enabled = true;
        text.text = msg;
        for (int x = 0; x < 32; x++)
        {
            text.GetComponent<RectTransform>().Translate(new Vector3(0, -1f));
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(5f);
        text.enabled = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, distance)) {
                foreach (KeyValuePair<int, Key> key in keys) {
                    if (hit.collider.gameObject == key.Value.KeyObject && !key.Value.PickedUp) {
                        fkeys[key.Key] = new Key(key.Value.KeyObject, key.Value.Info, key.Value.CanOpen);
                        fkeys[key.Key].PickedUp = true;
                        Destroy(fkeys[key.Key].KeyObject);
                        StartCoroutine(displaymessage($"You got: {fkeys[key.Key].Info}"));
                    }
                }
            }
            keys = fkeys;
        }
    }
}
