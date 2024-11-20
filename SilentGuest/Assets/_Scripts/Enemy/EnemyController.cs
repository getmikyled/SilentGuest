using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance { get; private set; }
    public Transform[] points; // points must be same height as Enemy
    private int current;

    public NavMeshAgent agent;
    public float stoppingDistance = 0.5f; // Leeway distance to consider position as arrived

    [Header("Components")] 
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    private static int SerialKiller_Forward_ParamID = Animator.StringToHash("Forward");

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
        if (points.Length > 0)
        {
            agent.SetDestination(points[current].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check if agent close enough to current point
        if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
        {
            current = (current + 1) % points.Length; // update current index
            agent.SetDestination(points[current].position);
        }
        
        _animator.SetFloat(SerialKiller_Forward_ParamID, _rigidbody.linearVelocity.normalized.magnitude);
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
}
