using System;
using System.Collections;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    // Layer Masks to ignore obstacles but target certain objects
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public GameManager gameManager;
    private bool isDead;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerRef.transform.position) < 2f && !isDead)
        {
            isDead = true;
            EnemyController.instance.StopMoving();
            gameManager.GameOver();
            Debug.Log("Player died");
        }
    }

    // coroutine
    private IEnumerator FOVRoutine()
    {
        // delay until search
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // if raycast does not hit a wall, it can see the player
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    OnPlayerSpotted();
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    void OnPlayerSpotted()
    {
        EnemyController.instance.MoveToPlayer(playerRef.transform);

        // Disable player movement and look toward enemy
        playerRef.GetComponent<PlayerMovement>().DisableMovement();
        StartCoroutine(LookAtEnemy());
    }

    private IEnumerator LookAtEnemy()
    {
        Vector3 directionToEnemy = (transform.position - playerRef.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        float duration = 3f; // time to complete rotation
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerRef.transform.rotation = Quaternion.Slerp(playerRef.transform.rotation, lookRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerRef.transform.rotation = lookRotation;
    }
}
