using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kusachki : MonoBehaviour
{
    public bool HaveKusachki = false;
    private RayPlayer rpl;

    private void Start()
    {
        rpl = GetComponent<RayPlayer>();
        rpl.OnPickUp += UselessPickupEvent;
        rpl.OnDrop += UselessDropEvent;
    }

    public void UselessPickupEvent()
    {
        if (rpl.theDest.transform.GetComponentInChildren<CustomInfo>()
            .tags
            .Contains("Kusachki"))
        {
            HaveKusachki = true;
        }
    }
    public void UselessDropEvent()
    {
        if (rpl.theDest.transform.GetComponentInChildren<CustomInfo>().tags.Contains("Kusachki"))
        {
            HaveKusachki = false;
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, 1.5f) && hit.collider.gameObject.CompareTag("Provoda") && HaveKusachki)
            {
                StartCoroutine(_destruction(hit.collider.gameObject));
            }
        }
    }

    private static IEnumerator _destruction(GameObject target)
    {
        target.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(2.5f);

        Destroy(target);
    }
}
