using UnityEngine;

public class AtticDialogue : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    
    void Start()
    {
        DialogueManager.instance.PlayDialogue(dialogue.dialogueLines);
    }
}
