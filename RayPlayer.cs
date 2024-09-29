using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class RayPlayer : MonoBehaviour
{
    public GameObject theDest;
    public bool pickup;
    public delegate void vd();
    public event vd OnDrop;
    public event vd OnPickUp;
    public string[] items;

    void Update()
    {
		if (pickup)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				theDest.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
				theDest.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
				theDest.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
				theDest.transform.GetChild(0).parent = null;
				pickup = false;
				OnDrop?.Invoke();
			}
		}

		RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if(hit.collider.CompareTag("Item"))
            {
				if (pickup == false)
				{
					if (Input.GetKeyDown(KeyCode.E))
					{
						hit.collider.transform.parent = theDest.transform;
						hit.collider.GetComponent<Rigidbody>().isKinematic = true;
						hit.collider.GetComponent<Rigidbody>().useGravity = false;
						hit.collider.GetComponent<BoxCollider>().enabled = false;
						hit.collider.transform.position = theDest.GetComponent<Transform>().position;
						hit.collider.transform.rotation = theDest.GetComponent<Transform>().rotation;
						pickup = true;
						
						OnPickUp?.Invoke();
					}
				}
            }
		}
	}
}
