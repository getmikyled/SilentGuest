using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayGlobalAudio(audioName: "mainmenu audio loop.wav", loop: true); 
        }
    }

}
