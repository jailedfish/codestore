using System.Collections;
using UnityEngine;

public class Biulding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainTestCase());
    }

    IEnumerator MainTestCase(){
        while(true) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(GetComponent<Transform>().position, worldPosition);
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
