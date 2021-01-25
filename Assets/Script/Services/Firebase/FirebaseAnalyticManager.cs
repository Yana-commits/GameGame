using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Firebase
{
    public class FirebaseAnalyticManager : IDisposable
    {
        private bool disposed = false;

        public FirebaseAnalyticManager()
        { }

        ~FirebaseAnalyticManager()
            => Dispose(false);

        public void Initialize()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }

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
            
            }
            // Free any unmanaged objects here.

            disposed = true;
        }

        #endregion


        #region Event

        public void AnalyticsLevelUp(int level)
        {
            FirebaseAnalytics.LogEvent(
              FirebaseAnalytics.EventLevelUp,
              new Parameter(FirebaseAnalytics.ParameterLevel, level));
        }

        #endregion
    }
}