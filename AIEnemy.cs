using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour {
    public delegate IEnumerator ic();
    [CanBeNull] public event ic OnDeath;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;
    private bool isWaiting = false;
    public float waitTime = 5f;
    public float distancePlayer = 7f;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            MoveToNextPatrolPoint();
        }
    }

    void Update()
    {
        if (!isWaiting && !agent.pathPending && agent.remainingDistance < 0.1f)
        {
            StartCoroutine(WaitBeforeNextPatrol());
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, distancePlayer);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (!IsPlayerVisible(collider.transform))
                {
                    return;
                }

                agent.destination = collider.transform.position;
            }
        }
    }

    private void OnCollisionEnter(Collision otherCollider) {
        if (otherCollider.gameObject.CompareTag("Player")) {
            StartCoroutine(otherCollider.gameObject.GetComponentInChildren<PlayerStaff>().OnDeath());
        }
    }

    bool IsPlayerVisible(Transform playerTransform)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, playerTransform.position, out hit))
        {
            if (hit.transform == playerTransform)
            {
                Vector3 directionToPlayer = playerTransform.position - transform.position;
                if (Vector3.Dot(transform.forward, directionToPlayer) > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator WaitBeforeNextPatrol()
    {
        isWaiting = true;
        anim.SetTrigger("Idle");
        anim.ResetTrigger("Walk");
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        MoveToNextPatrolPoint();
    }

    void MoveToNextPatrolPoint()
    {
        anim.SetTrigger("Walk");
        anim.ResetTrigger("Idle");

        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[currentPatrolIndex].position;
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

            Vector3 targetDirection = patrolPoints[currentPatrolIndex].position - transform.position;
            targetDirection.y = 0;

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, agent.angularSpeed * Time.deltaTime);
            }
        }
    }
}