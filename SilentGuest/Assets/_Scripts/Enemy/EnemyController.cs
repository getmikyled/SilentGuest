using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance { get; private set; }
    public Waypoint[] waypoints; // points must be same height as Enemy
    private int current;

    public NavMeshAgent agent;
    public float stoppingDistance = 0.5f; // Leeway distance to consider position as arrived

    private bool isWaiting = false;

    [Header("Components")] 
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    private static int SerialKiller_Forward_ParamID = Animator.StringToHash("Forward");
    private static int SerialKiller_Stab_StateID = Animator.StringToHash("SerialKiller_Stab");

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = 0;
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[current].point.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting)
        {
            // check if agent close enough to current point
            if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
            {
                if (waypoints[current].hasDelay)
                {
                    StartCoroutine(WaitAtWaypoint(waypoints[current].delayDuration));
                }
                else
                {
                    MoveToNextWaypoint();
                }
            }
            _animator.SetFloat(SerialKiller_Forward_ParamID, _rigidbody.linearVelocity.normalized.magnitude);
        }
        else
        {
            _animator.SetFloat(SerialKiller_Forward_ParamID, 0f);
        }
    }

    private IEnumerator WaitAtWaypoint(float delay)
    {
        isWaiting = true;

        agent.isStopped = true;
        _animator.SetFloat(SerialKiller_Forward_ParamID, 0f);

        yield return new WaitForSeconds(delay);

        agent.isStopped = false;

        MoveToNextWaypoint();

        isWaiting = false;
    }
    private void MoveToNextWaypoint()
    {
        current = (current + 1) % waypoints.Length; // update current index
        agent.SetDestination(waypoints[current].point.position);
    }

    public void MoveToPlayer(Transform playerTransform)
    {
        if (agent != null)
        {
            agent.SetDestination(playerTransform.position);
        }
    }
    
    public void StopMoving()
    {
        agent.speed = 0;
        // add killing animation
    }

    public void MurderPlayer()
    {
        StopMoving();
        
        // Play stab animation
        _animator.CrossFade(SerialKiller_Stab_StateID, 0.15f);
        AudioManager.instance.PlayGlobalAudio("death sound.wav");
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
}
