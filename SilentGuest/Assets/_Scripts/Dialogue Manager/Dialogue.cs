using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private string[] _dialogueLines;
    public string[] dialogueLines => _dialogueLines;
}
