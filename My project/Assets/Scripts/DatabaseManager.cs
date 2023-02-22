using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Firestore;

namespace Managers
{
    public class DatabaseManager : MonoBehaviour
    {
        FirebaseFirestore db;
        DocumentReference db_ref;
        ListenerRegistration listener;
        public Dictionary<string, object> currentUserData;
        void Start()
        {
            // db = FirebaseFirestore.DefaultInstance;
            // db_ref = db.Collection("UserData").Document("Dik6rdNOJFWEDCW3gYDin4ymXhs2");
            // db_ref.Listen(snapshot => {
            //     Debug.Log("Callback received document snapshot.");
            //     Debug.Log($"Document data for {snapshot.Id} document:");
            //     Dictionary<string, object> city = snapshot.ToDictionary();
            //     foreach (KeyValuePair<string, object> pair in city) {
            //         Debug.Log($"{pair.Key}: {pair.Value}");
            //     }
            // });
        }

        public void LoadUserData()
        {
            Debug.Log("Testing");
            AuthManager auth = MainManager.Instance.authManager;
            db = FirebaseFirestore.DefaultInstance;
            db_ref = db.Collection("UserData").Document(auth.user.UserId);
            listener = db_ref.Listen(snapshot => {
                Debug.Log("Callback received document snapshot.");
                Debug.Log($"Document data for {snapshot.Id} document:");
                Dictionary<string, object> userData = snapshot.ToDictionary();
                currentUserData = userData;
                foreach (KeyValuePair<string, object> pair in userData) {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }
            });
        }

        public void StopListening()
        {
            listener.Stop();
        }

        public void GetData()
        {
            db_ref.GetSnapshotAsync().ContinueWith( task =>
            {
                UserData data = task.Result.ConvertTo<UserData>();
                Debug.Log(data.DisplayName);
            });
        }

        public void UpdateData(Dictionary<string, object> data)
        {
            db_ref.UpdateAsync(data);
        }

        public void PurchaseItem()
        {
            
        }

    }
}

