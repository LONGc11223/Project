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
        [HideInInspector] public bool setup;

        void Awake()
        {
            Instance = this;

            authManager = GetComponent<AuthManager>();
            databaseManager = GetComponent<DatabaseManager>();
            setup = true;
        }
    }
}

