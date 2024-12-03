using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private float _textSpeed = 0.05f;
    
    private Coroutine playDialogueCoroutine;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlayDialogue(Dialogue dialogue)
    {
        // Stop coroutine if one is already playing
        if (playDialogueCoroutine != null)
        {
            StopCoroutine(playDialogueCoroutine);
            _textUI.gameObject.SetActive(false);
        }

        playDialogueCoroutine = StartCoroutine(CoPlayDialogue(dialogue.dialogueLines, 0));
    }
    
    public void PlayDialogue(string[] dialogueLines)
    {
        // Stop coroutine if one is already playing
        if (playDialogueCoroutine != null)
        {
            StopCoroutine(playDialogueCoroutine);
            _textUI.gameObject.SetActive(false);
        }

        playDialogueCoroutine = StartCoroutine(CoPlayDialogue(dialogueLines, 0));
    }

    private IEnumerator CoPlayDialogue(string[] dialogueLines, int dialogueIndex)
    {
        _textUI.gameObject.SetActive(true);
        
        string currentText = "";
        char[] chars = dialogueLines[dialogueIndex].ToCharArray();
        int currentChar = 0;

        while (currentText.Length < dialogueLines[dialogueIndex].Length)
        {
            currentText += chars[currentChar];
            currentChar++;

            _textUI.text = currentText;
            
            yield return new WaitForSeconds(_textSpeed);
        }
        
        yield return new WaitForSeconds(2);

        playDialogueCoroutine = null;
        _textUI.gameObject.SetActive(false);

        // Play next dialogue if there is one
        if (dialogueIndex + 1 < dialogueLines.Length)
        {
            playDialogueCoroutine = StartCoroutine(CoPlayDialogue(dialogueLines, dialogueIndex + 1));
        }
    }
}
