using UnityEngine;
using UnityEngine.Events;

public class HazardTrigger : MonoBehaviour
{

    [SerializeField] UnityEvent OnTriggerEnterPlayEvent;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            OnTriggerEnterPlayEvent.Invoke();
        }
    }

}
