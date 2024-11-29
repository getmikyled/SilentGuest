using UnityEngine;

public class Fridge : MonoBehaviour, InteractableEvent
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool triggerLevelObjective;
    
    private bool isOpen = false;

    private bool triggeredLevelObjective = false;
    
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
            
            // Trigger level objective
            if (triggerLevelObjective && triggeredLevelObjective == false)
            {
                triggeredLevelObjective = true;

                DialogueManager.instance.PlayDialogue("A head? Am I in the house of a murderer?");

                SwitchSceneOnInteract.instance.FinishedObjective();
            }
        }
    }

    public string GetInteractionPrompt()
    {
        if (isOpen)
        {
            return "Press E to Close Fridge";
        }
        else
        {
            return "Press E to Open Fridge";
        }
    }
}
