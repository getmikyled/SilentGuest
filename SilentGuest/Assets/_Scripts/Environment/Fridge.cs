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
            AudioManager.instance.PlayGlobalAudio("fridge close.wav");
            isOpen = false;
        }
        else
        {
            // Open the fridge
            _animator.CrossFade("Opening", 0.15f);
            AudioManager.instance.PlayGlobalAudio("fridge open.wav");
            AudioManager.instance.PlayGlobalAudio(audioName: "suspense sound.wav", loop: true);
            isOpen = true;
            
            // Trigger level objective
            if (triggerLevelObjective && triggeredLevelObjective == false)
            {
                triggeredLevelObjective = true;

                DialogueManager.instance.PlayDialogue(new string[]
                {
                    "A head? Am I in the house of a murderer?",
                    "I have to get out of here ASAP!"
                });

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
