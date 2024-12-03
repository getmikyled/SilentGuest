using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneOnInteract : MonoBehaviour, InteractableEvent
{
    public static SwitchSceneOnInteract instance;

    [SerializeField] private bool hasObjective = true;
    [SerializeField] private string interactionPrompt = "Press E to Interact";

    private bool finishedObjective = false;
    
    public string SceneName;
    
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

    public void typeEvent()
    {
        if (hasObjective == false || finishedObjective)
        {
            SceneManager.LoadScene(SceneName);
        }
        else if (hasObjective && finishedObjective == false)
        {
            DialogueManager.instance.PlayDialogue(new string[] {"I want to get some food first."});
        }
    }

    public void FinishedObjective()
    {
        finishedObjective = true;
    }

    public string GetInteractionPrompt()
    {
        return interactionPrompt;
    }
}
