using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider otherCollider) {
        Debug.Log(otherCollider.gameObject.tag);
        if (otherCollider.gameObject.CompareTag("Monster")) {
            transform.parent.gameObject.GetComponentInChildren<DoorController>().openDoor();
        }
    }

    private void OnTriggerExit(Collider otherCollider) {
        Debug.Log(otherCollider.gameObject.tag);
        if (otherCollider.gameObject.CompareTag("Monster")) {
            transform.parent.gameObject.GetComponentInChildren<DoorController>().closeDoor();
        }
    }
}
