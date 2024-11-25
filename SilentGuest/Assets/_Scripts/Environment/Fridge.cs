using UnityEngine;

public class Fridge : MonoBehaviour, InteractableEvent
{
    [SerializeField] private Animator _animator;
    
    private bool isOpen = false;
    
    public void typeEvent()
    {
        if (isOpen)
        {
            // If already open, close the fridge
            _animator.CrossFade("Closing", 0.15f);
            isOpen = false;
        }
        else
        {
            // Open the fridge
            _animator.CrossFade("Opening", 0.15f);
            isOpen = true;
        }
    }
}
