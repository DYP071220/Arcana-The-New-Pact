using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField UserNameInput;
    public InputField PasswordInput;
    public void OnLoginButtonClick()
    {
        string name=UserNameInput.text;
        string password=PasswordInput.text;
        if (name == "111" && password == "222")
        {
            SceneManager.LoadScene("Game");
        }
    }

}
