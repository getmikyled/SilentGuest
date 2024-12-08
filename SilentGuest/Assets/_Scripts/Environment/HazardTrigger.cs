using UnityEngine;
using UnityEngine.Events;

public class HazardTrigger : MonoBehaviour
{

    [SerializeField] UnityEvent OnTriggerEnterPlayEvent;

    void OnTriggerEnter(Collider other)
    {
        if (EnemyController.instance != null)
        {
            AudioManager.instance.PlayGlobalAudio("gasp");
            EnemyController.instance.MoveToPlayer(this.transform);
        }

        OnTriggerEnterPlayEvent.Invoke();
    }

}
