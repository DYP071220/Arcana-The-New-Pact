using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameStartButtonCTRL : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void QuitGame()
    {
        Debug.Log("ÍË³öÓÎÏ·");

    #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
#else 
            Application.Quit();
#endif
        Debug.Log("");
    }
}
