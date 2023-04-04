using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public Dictionary<string, object> inventory;
        public List<string> unlockedPets;
        public List<string> unlockedEnvironments;
        public DateTime lastMoveReward;
        public DateTime lastExerciseReward;

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
                    if (pair.Key == "Inventory")
                    {
                        inventory = (Dictionary<string,object>)pair.Value;
                    }
                    else if (pair.Key == "Rewards")
                    {
                        Dictionary<string,object> rewards = (Dictionary<string,object>)pair.Value;
                        // foreach (KeyValuePair<string, object> item in pair.Value.ToDictionary()) {
                        //     Debug.Log(item);
                        // }
                        Debug.Log(rewards["LastExerciseReward"]);
                        Debug.Log(rewards["LastMoveReward"]);
                    }
                }


                List<object> pets = (List<object>)inventory["Pets"];
                foreach (object item in pets) {
                    if (!unlockedPets.Contains(item.ToString())) {
                        unlockedPets.Add(item.ToString());
                    }
                }
                
                List<object> environments = (List<object>)inventory["Environments"];
                foreach (object item in environments) {
                    // Debug.Log($"TEST: {item}");
                    if (!unlockedEnvironments.Contains(item.ToString())) {
                        unlockedEnvironments.Add(item.ToString());
                    }
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

        public void PurchaseItem(string itemID, int cost, int type)
        {
            if (Convert.ToInt32(MainManager.Instance.databaseManager.currentUserData["Currency"]) >= cost)
            {
                Debug.Log("User has enough money!");
                DocumentReference docRef = db.Collection("UserData").Document(MainManager.Instance.authManager.user.UserId);

                if (type == 0)
                {
                    unlockedPets.Add(itemID);
                } 
                else if (type == 1)
                {
                    unlockedEnvironments.Add(itemID);
                }

                Dictionary<string, object> updateData = new Dictionary<string, object>
                {
                        { "Currency", Convert.ToInt32(MainManager.Instance.databaseManager.currentUserData["Currency"]) - cost },
                        { "Inventory", new Dictionary<string, object>
                            {
                                { "Environments", unlockedEnvironments},
                                { "Pets", unlockedPets}
                            }
                        }
                };
                docRef.UpdateAsync(updateData);
            }
            else
            {
                Debug.Log("User does not have enough money! ");
            }
            
        }

        public void AddFunds(int amount)
        {
            DocumentReference docRef = db.Collection("UserData").Document(MainManager.Instance.authManager.user.UserId);
            Dictionary<string, object> updateData = new Dictionary<string, object>
            {
                    { "Currency", Convert.ToInt32(MainManager.Instance.databaseManager.currentUserData["Currency"]) + amount },
            };
            docRef.UpdateAsync(updateData);
        }

    }
}

