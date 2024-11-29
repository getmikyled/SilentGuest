using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string levelName = "3_Level";
    
    public void PlayButton(){
        SceneManager.LoadScene(levelName);
    }
}
