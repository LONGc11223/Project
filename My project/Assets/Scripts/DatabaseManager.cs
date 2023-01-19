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
        void Start()
        {
            db = FirebaseFirestore.DefaultInstance;
            db_ref = db.Collection("UserData").Document("Dik6rdNOJFWEDCW3gYDin4ymXhs2");
            db_ref.Listen(snapshot => {
                Debug.Log("Callback received document snapshot.");
                Debug.Log($"Document data for {snapshot.Id} document:");
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city) {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }
            });
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

    }
}

