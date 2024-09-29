using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;

    public float swingAngle = 10f;
    public float swingSpeed = 2f;

    private float originalYRotation;

    void Start()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        originalYRotation = camTransform.localEulerAngles.y;
    }

    void Update()
    {
        float swingAmount = Input.GetAxis("Horizontal") * swingAngle;
        float targetYRotation = originalYRotation + swingAmount;

        Quaternion targetRotation = Quaternion.Euler(camTransform.localEulerAngles.x, targetYRotation, camTransform.localEulerAngles.z);
        camTransform.localRotation = Quaternion.Slerp(camTransform.localRotation, targetRotation, swingSpeed * Time.deltaTime); 
    }
}