using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    public static class Util
    {
        public static T GetComponentInChildrenRecursive<T>(Transform obj) where T : MonoBehaviour
        {
            Log.DebugLog("CheckStart : " + obj.gameObject.name);
            if(obj.GetComponent<T>() != null)
            {
                return obj.GetComponent<T>();
            }
            else
            {
                for(int i = 0; i < obj.childCount; i++)
                {
                    T tryGetT = GetComponentInChildrenRecursive<T>(obj.GetChild(i));

                    if (tryGetT != null)
                    {
                        Log.DebugLog("CheckEnd : " + obj.gameObject.name);
                        return tryGetT;
                    }
                }
                Log.DebugLog("NotFound : " + obj.gameObject.name);
                return null;
            }
        }
    }
}
