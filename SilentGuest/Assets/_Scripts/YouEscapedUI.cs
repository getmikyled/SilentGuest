using UnityEngine;
using UnityEngine.SceneManagement;

public class YouEscapedUI : MonoBehaviour
{
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
