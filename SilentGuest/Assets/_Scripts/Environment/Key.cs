using UnityEngine;

public class Key : MonoBehaviour, InteractableEvent
{
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject playerKey;

    public void typeEvent(){
        PlayerMovement.instance.hasKey = true;
        key.SetActive(false);
        playerKey.SetActive(true);
    }

    public string GetInteractionPrompt(){
        return "Press E To Pick Up";
    }
}
