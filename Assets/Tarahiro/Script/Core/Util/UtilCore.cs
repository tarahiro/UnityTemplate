using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    public static class UtilCore
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

        public static bool IsTrue(string s)
        {
            if(s == Tarahiro.Const.c_true)
            {
                return true;
            }
            else if (s == Tarahiro.Const.c_false)
            {
                return false;
            }
            else
            {
                if (s.ToLower() == Tarahiro.Const.c_true.ToLower())
                {
                    Log.DebugWarning("stringの表記が正確ではありません。" + Tarahiro.Const.c_true + "と表記してください");
                    return true;
                }
                else if (s.ToLower() == Tarahiro.Const.c_false.ToLower())
                {
                    Log.DebugWarning("stringの表記が正確ではありません。" + Tarahiro.Const.c_false + "と表記してください");
                    return false;

                }
                throw new ArgumentException("Invalid string value. Expected 'true' or 'false'.");
            }
        }

        public static List<int> RandomOrder(int count)
        {
            List<int> numbers = Enumerable.Range(0, count).ToList();
            System.Random rng = new System.Random();
            int n = numbers.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = numbers[n];
                numbers[n] = numbers[k];
                numbers[k] = temp;
            }
            return numbers;
        }
    }
}
