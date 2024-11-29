using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private float _textSpeed = 0.05f;

    private string text = "";

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

    public void PlayDialogue(string dialogue)
    {
        text = dialogue;

        // Stop coroutine if one is already playing
        if (playDialogueCoroutine != null)
        {
            StopCoroutine(playDialogueCoroutine);
            _textUI.gameObject.SetActive(false);
        }

        playDialogueCoroutine = StartCoroutine(CoPlayDialogue());
    }

    private IEnumerator CoPlayDialogue()
    {
        _textUI.gameObject.SetActive(true);
        
        string currentText = "";
        char[] chars = text.ToCharArray();
        int currentChar = 0;

        while (currentText.Length < text.Length)
        {
            currentText += chars[currentChar];
            currentChar++;

            _textUI.text = currentText;
            
            yield return new WaitForSeconds(_textSpeed);
        }
        
        yield return new WaitForSeconds(2);

        playDialogueCoroutine = null;
        _textUI.gameObject.SetActive(false);
    }
}
