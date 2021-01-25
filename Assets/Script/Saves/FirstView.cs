using Services.Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Game.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstView : MonoBehaviour
{
    private IEnumerator Start()
    {
        var auth = FirebaseManager.authManager;
        auth.OnUserChanged += OnUserChanged;

        yield return new WaitUntil(() => auth.IsInitialized);

        if (auth.User == null)
            _ = auth.SignInAnonymously();
    }

    private async void OnUserChanged(FirebaseUser obj)
    {
        if (obj == null)
        {
            UserDataController.Instance().info = null;
        }
        else
        {
            var db = FirebaseManager.databaseManager;

            var d = await db.GetUserData<UserData>();
            if (d.currentLvl != UserDataController.Instance().info.currentLvl)
                d = UserDataController.Instance().info;
            UserDataController.Instance().info = d;

            //_ = db.SetUserData(d);
        }

        if (UserDataController.Instance().info != null)
        {
            SceneManager.LoadScene("LoginScene", LoadSceneMode.Single);
        }
    }
}
