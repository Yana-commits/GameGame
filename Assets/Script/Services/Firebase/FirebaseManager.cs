using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace Services.Firebase
{
    public static class FirebaseManager
    {
        public static readonly FirebaseDatabaseManager databaseManager;
        public static readonly FirebaseAuthManager authManager;
        public static readonly FirebaseAnalyticManager analyticManager;

        static FirebaseManager()
        {
            databaseManager = new FirebaseDatabaseManager();
            authManager = new FirebaseAuthManager();
            analyticManager = new FirebaseAnalyticManager();

            authManager.OnUserChanged += databaseManager.AuthStateChanged;
            databaseManager.AuthStateChanged(authManager.User);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
#if UNITY_IOS || UNITY_IPHONE
            UnityEngine.iOS.Device.SetNoBackupFlag(Application.persistentDataPath);
#endif

            FirebaseApp.CheckAndFixDependenciesAsync()
               .ContinueWithOnMainThread(task =>
               {
                   var dependencyStatus = task.Result;
                   if (dependencyStatus == DependencyStatus.Available)
                       InitializeFirebase();
                   else
                       Debug.LogError(
                     "Could not resolve all Firebase dependencies: " + dependencyStatus);
               });
        }

        private static void InitializeFirebase()
        {
            analyticManager?.Initialize();
            databaseManager?.Initialize();
            authManager?.Initialize();
        }
    }
}
