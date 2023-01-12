using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Managers;
using UnityEngine.SceneManagement;

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
        if (AuthManager.active == true && SceneManager.GetActiveScene().buildIndex == 1)
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
            AuthManager auth = MainManager.Instance.authManager;

            StartCoroutine(auth.SignUp(email, password));
        }
        else
        {
            Debug.Log("Passwords do not match!");
        }
    }
    
    public void SignOutButton()
    {
        AuthManager auth = MainManager.Instance.authManager;
        Debug.Log("Attempting to login the user!");
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
}
