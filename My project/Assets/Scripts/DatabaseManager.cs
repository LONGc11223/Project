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
        void Start()
        {
            db = FirebaseFirestore.DefaultInstance;
        }

        public void GetData()
        {
            db.Collection("UserData").Document("Dik6rdNOJFWEDCW3gYDin4ymXhs2").GetSnapshotAsync().ContinueWith( task =>
            {
                UserData data = task.Result.ConvertTo<UserData>();
                Debug.Log(data.DisplayName);
            });
        }

    }
}

