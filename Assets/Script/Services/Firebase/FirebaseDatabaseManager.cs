using System;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Firebase;
using System.Collections.Generic;
using Game.Data;
using System.Linq;
using UnityEngine;

namespace Services.Firebase
{
    public class FirebaseDatabaseManager : IDisposable
    {
        private readonly static string usersPath = "users";

        public DatabaseReference userReference { get; private set; }

        private bool disposed = false;

        public FirebaseDatabaseManager()
        { }
        ~FirebaseDatabaseManager()
            => Dispose(false);

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                // Free any other managed objects here.
                userReference = null;
            }
            // Free any unmanaged objects here.

            disposed = true;
        }

        #endregion

        #region Initialize

        public void Initialize()
        {
#if !UNITY_EDITOR
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);
#endif
        }

        #endregion

        #region Firebase callback

        public void AuthStateChanged(FirebaseUser user)
        {
            if (user != null)
            {
                userReference = FirebaseDatabase.DefaultInstance
                    .GetReference($"{usersPath}/{user.UserId}");
                userReference.KeepSynced(true);

            }
            else
                userReference = null;
        }

        #endregion

        #region Get data

        public async Task<T> GetUserData<T>() where T : new()
        {
            if (userReference == null) return default;

            try
            {
                var snapshot = await userReference.GetValueAsync();
                string json = snapshot.GetRawJsonValue();

                if (string.IsNullOrEmpty(json))
                    return new T();

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (FirebaseException ex)
            {
                throw ex;
            }
        }

        public Task SetUserData(object value)
        {
            var json = JsonConvert.SerializeObject(value);
            return SetUserData(json);
        }

        public async Task SetUserData(string json)
        {
            if (userReference == null) return;

            try
            {
               await userReference.SetRawJsonValueAsync(json);
            }
            catch (FirebaseException ex)
            {
                throw ex;
            }
        }

        #endregion
        public async Task<List<UserData>> GetLeaderBoard(int leadersAmount)
        {
            List<UserData> leaders = new List<UserData>();
            try
            {
                var snapshot = await FirebaseDatabase.DefaultInstance
                    .GetReference("users")
                    .OrderByChild("score")
                    .LimitToLast(leadersAmount)
                    .GetValueAsync();

                if (snapshot.Exists)
                {
                    foreach (var child in snapshot.Children.Reverse())
                    {
                        leaders.Add(JsonConvert.DeserializeObject<UserData>(child.GetRawJsonValue()));
                    }
                }
            }
            catch (FirebaseException e)
            {
                Debug.Log(e);
            }

            return leaders;
        }
    }
}    
