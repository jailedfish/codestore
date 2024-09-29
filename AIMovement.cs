using System.Collections;
using UnityEngine;

public class RandomMovementWithDelay : MonoBehaviour
{
    public float speed = 5f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;
    public float delayTime = 5f;

    private Vector3 targetPosition;
    private bool isMoving = true;

    void Start()
    {
        SetRandomTargetPosition();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
                StartCoroutine(DelayBeforeNextMove());
            }
        }
    }

    IEnumerator DelayBeforeNextMove()
    {
        yield return new WaitForSeconds(delayTime);
        isMoving = true;
        SetRandomTargetPosition();
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }
}