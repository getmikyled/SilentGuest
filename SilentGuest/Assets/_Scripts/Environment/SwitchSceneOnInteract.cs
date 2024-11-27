using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneOnInteract : MonoBehaviour, InteractableEvent
{
    public string SceneName;

    public void typeEvent(){
        SceneManager.LoadScene(SceneName);
    }
}
