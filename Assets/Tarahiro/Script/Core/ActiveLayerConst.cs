using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    public static class ActiveLayerConst
    {

        public enum InputLayer
        {
            None = 0,
            Title = 3,
            FreeInput = 5,
            Typing = 10,
            ClickInput = 15,
            Conversation = 20,
            Skill = 30,
            SettingMenu = 100,
            DevelopmentMenu = 150,
            SettingMenuItem = 200,
            SettingConversation = 300,
            Helper = 500,
            HelperConversation = 530,
        
            GameOver = 1000,
            Develop = 2000,
        }
    }
}