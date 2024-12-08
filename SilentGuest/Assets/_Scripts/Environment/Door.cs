using UnityEngine;

public class Door : MonoBehaviour, InteractableEvent
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private GameObject key;
    
    private bool isOpen = false;
    
    public void typeEvent()
    {
        // Return if the door is locked
        if (isLocked)
        {
            if(PlayerMovement.instance.hasKey == true){
                PlayerMovement.instance.hasKey = false;
                isLocked = false;
                key.SetActive(false);
            }
            else
            {
                DialogueManager.instance.PlayDialogue(new string[]{"Hm. There must be a key somewhere."});
            }

            return;
        }
        
        if (isOpen)
        {
            // If already open, close the fridge
            _animator.CrossFade("Closing", 0.15f);
            AudioManager.instance.PlayGlobalAudio("fridge close");
            isOpen = false;
        }
        else
        {
            // Open the fridge
            _animator.CrossFade("Opening", 0.15f);
            AudioManager.instance.PlayGlobalAudio("fridge open");
            isOpen = true;
        }
    }

    public string GetInteractionPrompt()
    {
        if (isLocked)
        {
            if(PlayerMovement.instance.hasKey == true){
                return "Press E to Unlock Door";
            }
            else{
                return "Locked.";
            }
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
