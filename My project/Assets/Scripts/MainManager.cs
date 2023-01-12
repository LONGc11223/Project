using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;
        public AuthManager authManager;
        public DatabaseManager databaseManager;

        void Awake()
        {
            Instance = this;

            authManager = GetComponent<AuthManager>();
            databaseManager = GetComponent<DatabaseManager>();
        }
    }
}

