using UnityEngine;
using System;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90.0f;
    public float closeAngle = 0.0f;
    public float smooth = 2.0f;
    public float distance = 2.0f;

    public AudioClip openSound;
    public AudioClip closeSound;
    public GameObject player;
    public string keyTagNeeded;
    public bool isClosed = false;

    private bool isOpen = false;
    private bool isMoving = false;
    private Vector3 defaultRot;
    private AudioSource audioSource;
    

    void Start()
    {
        defaultRot = transform.localEulerAngles;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, distance) && hit.collider.gameObject == gameObject && !isMoving ) {
            ShowObjectsOnDoorHover();
            if (Input.GetMouseButtonDown(0)) {
                if (player.GetComponent<KeyLogic>().HaveKey(keyTagNeeded) || !isClosed) {
                    isMoving = true;
                    if (isOpen) {
                        closeDoor();
                    }
                    else {
                        openDoor();
                    }
                    
                }
            }
        }
        else
        {
            HideObjectsOnDoorHover();
        }
    }

    public void openDoor() {
        if (!isOpen) {
            StartCoroutine(AnimateDoor(defaultRot, openAngle));
            PlaySound(closeSound);
            isOpen = !isOpen;
        }
        
    }

    public void closeDoor() {
        if (isOpen) {
            Debug.Log("closing door");
            StartCoroutine(AnimateDoor(defaultRot, closeAngle));
            PlaySound(openSound);
            isOpen = !isOpen;
        }
    }

    void ShowObjectsOnDoorHover()
    {
    }

    void HideObjectsOnDoorHover()
    {
    }

    IEnumerator AnimateDoor(Vector3 from, float to)
    {
        float t = 0.0f;
        float startAngle = transform.localEulerAngles.y;
        while (t < 1.0f)
        {
            t += Time.deltaTime * smooth;
            float angle = Mathf.LerpAngle(startAngle, to, t);
            transform.localEulerAngles = new Vector3(defaultRot.x, angle, defaultRot.z);
            yield return null;
        }
        isMoving = false;
    }

    void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}