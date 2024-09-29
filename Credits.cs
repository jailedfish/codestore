using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject credit;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCredits()
    {
        StartCoroutine(StartCreditus());
    }

    IEnumerator StartCreditus()
    {
        credit.SetActive(true);
        yield return new WaitForSeconds(12);
        credit.SetActive(false);
    }
}
