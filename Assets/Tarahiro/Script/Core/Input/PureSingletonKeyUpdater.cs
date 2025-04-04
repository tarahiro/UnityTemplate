using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    public class PureSingletonKeyUpdater : IPostInitializable, ITickable
    {

        [Inject] PureSingletonKey _key;
        
        public void PostInitialize()
        {
            _key.Initialize();
        }

        public void Tick()
        {
            _key.TryReset();

            foreach (KeyCode key in PureSingletonKey.InputtableKeyList)
            {
                if (Input.GetKeyDown(key))
                {
                    _key.AddKeyDown(key);
                }
                if (Input.GetKey(key)) _key.AddKey(key);
                if (Input.GetKeyUp(key)) _key.AddKeyUp(key);
            }

            _key.SetStrokedKey(Input.inputString);
        }

    }
}
