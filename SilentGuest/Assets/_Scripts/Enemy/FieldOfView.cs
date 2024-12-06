using System;
using System.Collections;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    // Layer Masks to ignore obstacles but target certain objects
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    
    private bool isDead;

    [SerializeField] private Transform headLookAt;
    
    [SerializeField] private float distanceFromPlayerForGameOver = 0.5f;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if (PlayerMovement.instance != null && Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < distanceFromPlayerForGameOver && !isDead)
        {
            isDead = true;
            EnemyController.instance.MurderPlayer();
            PostProcessManager.instance.PlayDeathEffect();
            GameManager.instance.GameOver();
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
        EnemyController.instance.MoveToPlayer(PlayerMovement.instance.transform);
        AudioManager.instance.PlayGlobalAudio("sk catches you.wav");
        AudioManager.instance.PlayGlobalAudio("chase sound.wav");

        // Disable player movement and look toward enemy
        PlayerMovement.instance.DisableMovement();
        StartCoroutine(UpdateLookRotations());
    }

    private IEnumerator UpdateLookRotations()
    {
        Vector3 directionToEnemy = (headLookAt.transform.position - PlayerMovement.instance.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        float duration = 3f; // time to complete rotation
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            PlayerMovement.instance.transform.rotation = Quaternion.Slerp(PlayerMovement.instance.transform.rotation, lookRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        

        PlayerMovement.instance.transform.rotation = lookRotation;
    }
}
