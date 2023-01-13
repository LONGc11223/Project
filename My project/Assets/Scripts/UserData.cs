using Firebase.Firestore;
using System.Collections.Generic;

[FirestoreData]
public struct UserData
{
    [FirestoreProperty]
    public string DisplayName { get; set; }

    [FirestoreProperty]
    public Dictionary<string, object> HealthData { get; set; }

    [FirestoreProperty]

    public Dictionary<string,string> PetData { get; set; }
}
