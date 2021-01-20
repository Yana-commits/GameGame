﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public class UserData
    {
        public string Name = "Name";
        public int index = 0;
        public int currentLvl = 0;
        public int score = 0;

        public UserData()
        { 
        }

        public UserData(string name)
        {
         Name = name;
         index = 0;
         currentLvl = 0;
         score = 0;
        }
    }
}
