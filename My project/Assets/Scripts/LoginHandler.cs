using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Managers;

public class LoginHandler : MonoBehaviour
{
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    [Header("Sign Up")]
    public TMP_InputField emailSignUpField;
    public TMP_InputField passwordSignUpField;
    public TMP_InputField confirmSignUpField;

    void Update()
    {
        if (AuthManager.active == true && SceneManager.GetActiveScene().name == "Login Screen")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        

    }

    public void SignUpButton()
    {
        string email = emailSignUpField.text;
        string password = passwordSignUpField.text;
        string confirmPassword = confirmSignUpField.text;

        if (password == confirmPassword)
        {
            Debug.Log("Signup Success!");
            AuthManager auth = MainManager.Instance.authManager;

            // StartCoroutine(auth.SignUp(email, password));
            auth.SignUpAlt(email, password);
        }
        else
        {
            Debug.Log("Passwords do not match!");
        }
    }
    
    public void SignOutButton()
    {
        AuthManager auth = MainManager.Instance.authManager;
        Debug.Log("Attempting to logout the user!");
        auth.SignOut();
    }
    public void LoginButton()
    {
        string email = emailLoginField.text;

        string password = passwordLoginField.text; 

        AuthManager auth = MainManager.Instance.authManager;
        Debug.Log("Attempting to login the user!");
        StartCoroutine(auth.Login(email,password));
    }

    public void GoToInfo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
