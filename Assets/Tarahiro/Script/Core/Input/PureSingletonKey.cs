using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using System.Linq;
using UnityEngine.UIElements;

namespace Tarahiro
{
    public class PureSingletonKey
    {
        Dictionary<KeyInputState, List<KeyCode>> _keyListArray = new Dictionary<KeyInputState, List<KeyCode>>();

        Dictionary<KeyCode, float> _pressingTimeSince = new Dictionary<KeyCode, float>();

        string _strokedKey { get; set; }
 
        [Inject] PureSingletonInput _input;


        public void Initialize()
        {
            TryReset();
        }


        public void TryReset()
        {
            foreach (KeyInputState key in Enum.GetValues(typeof(KeyInputState)))
            {
                if (!_keyListArray.ContainsKey(key))
                {
                    _keyListArray.Add(key, new List<KeyCode>());
                }
                else if (_keyListArray[key].Count > 0)
                {
                    _keyListArray[key] = new List<KeyCode>();
                }
            }

            _strokedKey = "";
        }


        public void AddKeyDown(KeyCode keycode)
        {
            RegisterKey(KeyInputState.Down, keycode);
            _pressingTimeSince.Add(keycode, Time.time);
        }
        public void AddKey(KeyCode keycode)
        {
            RegisterKey(KeyInputState.Key, keycode);
        }
        public void AddKeyUp(KeyCode keycode)
        {
            RegisterKey(KeyInputState.Up, keycode);
            _pressingTimeSince.Remove(keycode);
        }

        public bool IsKeyDown(KeyCode key)
        {
            return IsKeyState(KeyInputState.Down, key);
        }

        public bool IsKey(KeyCode key)
        {
            return IsKeyState(KeyInputState.Key, key);
        }

        public bool IsKeyUp(KeyCode key)
        {
            return IsKeyState(KeyInputState.Up, key);
        }
        public bool TryGetPressingTimeSince(KeyCode key, out float sinceTime)
        {
            if (_pressingTimeSince.ContainsKey(key))
            {
                sinceTime = _pressingTimeSince[key];
                return true;
            }
            else
            {
                sinceTime = 0f;
                return false;
            }
        }

        public string GetStrokedKey()
        {
            if (_input.IsAnyAvailableInputted) return "";
            return _strokedKey;
        }

        public void SetStrokedKey(string strokedKey)
        {
            _strokedKey = strokedKey;
        }

        void RegisterKey(KeyInputState inputState, KeyCode keycode)
        {
            _keyListArray[inputState].Add(keycode);
        }

        bool IsKeyState(KeyInputState inputState, KeyCode key)
        {
            if (_input.IsAnyAvailableInputted) return false;

            return _keyListArray[inputState].Any(x => x == key);

        }


        public readonly static List<KeyCode> InputtableKeyList = new List<KeyCode>()
        {
            KeyCode.Alpha0,
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,

            KeyCode.A,
            KeyCode.B,
            KeyCode.C,
            KeyCode.D,
            KeyCode.E,
            KeyCode.F,
            KeyCode.G,
            KeyCode.H,
            KeyCode.I,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L,
            KeyCode.M,
            KeyCode.N,
            KeyCode.O,
            KeyCode.P,
            KeyCode.Q,
            KeyCode.R,
            KeyCode.S,
            KeyCode.T,
            KeyCode.U,
            KeyCode.V,
            KeyCode.W,
            KeyCode.X,
            KeyCode.Y,
            KeyCode.Z,
            KeyCode.Minus,
            KeyCode.Caret,
            KeyCode.Backslash,
            KeyCode.At,
            KeyCode.LeftBracket,
            KeyCode.Semicolon,
            KeyCode.Colon,
            KeyCode.RightBracket,
            KeyCode.Comma,
            KeyCode.Period,
            KeyCode.Slash,
            KeyCode.Underscore,
            KeyCode.Backspace,
            KeyCode.Return,
            KeyCode.Space,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
            KeyCode.DownArrow,
            KeyCode.UpArrow,
        };

        public enum KeyInputState
        {
            Down,
            Key,
            Up
        }

    }
}
