using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class AuthManager : MonoBehaviour
    {
        public DependencyStatus dependencyStatus;
        public static bool active;
        public FirebaseAuth auth;
        public FirebaseUser user;

        void Start()
        {
            while (!MainManager.Instance.setup) {}
            InitializeFirebase();

            // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith( task =>
            // {
            //     dependencyStatus = task.Result;

            //     if (dependencyStatus == DependencyStatus.Available)
            //     {
            //         auth = FirebaseAuth.DefaultInstance;

            //         auth.StateChanged += AuthStateChanged;
            //         AuthStateChanged(this, null);
            //     }
            //     else
            //     {
            //         Debug.LogError("Missing Dependencies!");
            //     }
            // });
        }

        void InitializeFirebase() {
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }

        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (auth.CurrentUser != user)
            {
                bool loggedIn = (user != auth.CurrentUser && auth.CurrentUser != null);

                // Handle when a user logs out
                if (!loggedIn && user != null)
                {
                    Debug.Log("User logged out");
                    active = false;
                }

                user = auth.CurrentUser;

                // Handle when user is logged in
                if (loggedIn)
                {
                    Debug.Log("User signed in");
                    active = true;
                }
            }
        }

        public IEnumerator SignUp(string email, string password)
        {
            // TODO: add checks beforehand to see if they entered valid info

            var signupTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            // Wait until user is created
            yield return new WaitUntil(predicate: () => signupTask.IsCompleted);

            if (signupTask.Exception == null)
            {
                user = signupTask.Result;
                if (user != null)
                {
                    // Do database thing here
                }
            }

        }

        public IEnumerator Login(string email, string password)
        {
            var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
            // Wait until user is created
            Debug.Log("Waiting");
            yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

            if (loginTask.Exception == null)
            {
                Debug.Log($"User logged in successfully!");
                user = loginTask.Result;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        public void SignOut()
        {
            auth.SignOut();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
    }
}

