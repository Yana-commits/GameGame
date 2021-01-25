using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Services.Firebase;

namespace Game.Data
{
    public class UserDataController
    {
        private static UserDataController instance;

        public static UserDataController Instance()
        {
            if (instance == null)
            {
                instance = new UserDataController();
            }
            return instance;
        }

        private UserDataController()
        {
            info = new UserData("MMM");
            Load();

        }
        public UserData info;

        public void LocalSave()
        {
            string path = Application.persistentDataPath + "/user";
            string json = JsonConvert.SerializeObject(info);
            Debug.Log($"{path}");
            File.WriteAllText(path, json);

            _ = FirebaseManager.databaseManager.SetUserData(json);
        }

        public void Load()
        {

            string path = Application.persistentDataPath + "/user";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(json))
                {
                    info = JsonConvert.DeserializeObject<UserData>(json);

                    return;
                }
            }

            info = new UserData("MMM");
        }


    }
}

