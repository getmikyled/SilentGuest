using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string levelName = "1_Attic";
    private GameObject mainMenuAudio;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainMenuAudio = AudioManager.instance.PlayGlobalAudio(audioName: "mainmenu audio loop", loop: true);
    }

    public void PlayButton(){
        if (mainMenuAudio != null)
        {
            AudioManager.instance.StopAudio(mainMenuAudio);
        }

        SceneManager.LoadScene(levelName);
    }
}
