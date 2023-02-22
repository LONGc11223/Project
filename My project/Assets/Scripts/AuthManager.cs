using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
// using Firebase.Firestore;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Extensions;

namespace Managers
{
    public class AuthManager : MonoBehaviour
    {
        public DependencyStatus dependencyStatus;
        public static bool active;
        public FirebaseAuth auth;
        public FirebaseUser user;
        FirebaseFirestore db;

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
            db = FirebaseFirestore.DefaultInstance;
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
            Debug.Log("A!");
            var signupTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            // Wait until user is created
            yield return new WaitUntil(predicate: () => signupTask.IsCompleted);

            Debug.Log("B!");

            if (signupTask.Exception == null)
            {
                user = signupTask.Result;
                if (user != null)
                {
                    Debug.Log("Attempting to create new data!");
                    // Do database thing here
                    DocumentReference docRef = db.Collection("UserData").Document(user.UserId);
                    Dictionary<string, object> initialData = new Dictionary<string, object>
                    {
                            { "Currency", 0 },
                            { "Environment", "Original" },
                            { "HealthData", new Dictionary<string, object>
                                {
                                    { "CaloriesBurned", 0 },
                                    { "ExerciseMinutes", 0 },
                                }
                            },
                            { "PetData", new Dictionary<string, object>
                                {
                                    { "Mood", "Neutral" },
                                    { "PetAppearance", "GoldenRetriever" },
                                }
                            },
                            { "Inventory", new Dictionary<string, object>
                                {
                                    { "Environments", new List<object>() { "Original" }},
                                    { "Pets", new List<object>() { "GoldenRetriever" }}
                                }
                            }
                    };
                    docRef.SetAsync(initialData).ContinueWithOnMainThread(task => {
                            Debug.Log("User's save data created!.");
                    });
                }
            }
            Debug.Log("C!");
        }

        public void SignUpAlt(string email, string password)
        {
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            user = task.Result;
            // Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            //     user.DisplayName, newUser.UserId);
            Debug.Log("User Created!");
            
            Debug.Log("Attempting to create new data!");
            // Do database thing here
            DocumentReference docRef = db.Collection("UserData").Document(user.UserId);
            Dictionary<string, object> initialData = new Dictionary<string, object>
            {
                    { "Currency", 0 },
                    { "Environment", "Original" },
                    { "HealthData", new Dictionary<string, object>
                        {
                            { "CaloriesBurned", 0 },
                            { "ExerciseMinutes", 0 },
                        }
                    },
                    { "PetData", new Dictionary<string, object>
                        {
                            { "Mood", "Neutral" },
                            { "PetAppearance", "GoldenRetriever" },
                        }
                    },
                    { "Inventory", new Dictionary<string, object>
                        {
                            { "Environments", new List<object>() { "Original" }},
                            { "Pets", new List<object>() { "GoldenRetriever" }}
                        }
                    }
            };
            docRef.SetAsync(initialData).ContinueWithOnMainThread(task => {
                    Debug.Log("User's save data created!.");
            });

            });
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
            MainManager.Instance.databaseManager.StopListening();
            auth.SignOut();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
    }
}

