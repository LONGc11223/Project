using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Managers;

namespace Managers
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;
        public AuthManager authManager;
        public DatabaseManager databaseManager;
        public HealthManager healthManager;
        public NotificationManager notificationManager;
        [HideInInspector] public bool setup;

        void Awake()
        {
            Instance = this;

            authManager = GetComponent<AuthManager>();
            databaseManager = GetComponent<DatabaseManager>();
            healthManager = GetComponent<HealthManager>();
            notificationManager = GetComponent<NotificationManager>();
            setup = true;
        }
    }
}

