using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public AudioSource buttonHover;
    public AudioClip hoverSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hover()
    {
        buttonHover.PlayOneShot(hoverSound);
    }
}
