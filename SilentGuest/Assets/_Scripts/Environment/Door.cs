using UnityEngine;

public class Door : MonoBehaviour, InteractableEvent
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool isLocked = false;
    
    private bool isOpen = false;
    
    public void typeEvent()
    {
        // Return if the door is locked
        if (isLocked)
        {
            return;
        }
        
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

    public string GetInteractionPrompt()
    {
        if (isLocked)
        {
            return "Locked.";
        }
        else if (isOpen)
        {
            return "Press E to Close Door";
        }
        else
        {
            return "Press E to Open Door";
        }
    }
}
