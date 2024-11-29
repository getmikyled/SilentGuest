using TMPro;
using UnityEngine;

public class InteractionPromptManager : MonoBehaviour
{
    public static InteractionPromptManager instance;

    [SerializeField] private TextMeshProUGUI _textUI;
    
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

    public void SetInteractionPrompt(string text)
    {
        _textUI.text = text;
        _textUI.gameObject.SetActive(true);
    }

    public void DeactivateInteractionPrompt()
    {
        _textUI.gameObject.SetActive(false);
    }
}
