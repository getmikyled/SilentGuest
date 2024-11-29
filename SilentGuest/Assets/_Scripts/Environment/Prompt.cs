using UnityEngine;

public class Prompt : MonoBehaviour, InteractableEvent
{
    public void typeEvent()
    {
        
    }

    public string GetInteractionPrompt()
    {
        return "Press E to Interact";
    }
}
